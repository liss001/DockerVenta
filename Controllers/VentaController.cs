using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DockerVenta.Data;
using DockerVenta.Models;

namespace DockerVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly VentaDb _db;
        public VentaController(VentaDb db) => _db = db;

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas() =>
            Ok(await _db.Ventas.Include(v => v.Cliente)
                                .Include(v => v.Items)
                                .ThenInclude(i => i.Producto)
                                .ToListAsync());

        [HttpPost("Registrar")]
        public async Task<ActionResult<Venta>> RegistrarVenta(Venta venta)
        {
            var cliente = await _db.Clientes.FindAsync(venta.ClienteId);
            if (cliente == null) return BadRequest("Cliente no encontrado");

            decimal total = 0;

            foreach (var item in venta.Items!)
            {
                var producto = await _db.Productos.FindAsync(item.ProductoId);
                if (producto == null) return BadRequest($"Producto Id {item.ProductoId} no encontrado");
                if (producto.Stock < item.Cantidad) return BadRequest($"Stock insuficiente para {producto.Nombre}");

                item.PrecioUnitario = producto.Precio;
                item.SubTotal = producto.Precio * item.Cantidad;
                total += item.SubTotal;

                producto.Stock -= item.Cantidad;
            }

            venta.Fecha = DateTime.Now;
            venta.Total = total;

            _db.Ventas.Add(venta);
            await _db.SaveChangesAsync();

            return Ok(venta);
        }
    }
}
