using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DockerVenta.Data;
using DockerVenta.Models;

namespace DockerVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly VentaDb _db;
        public ProductoController(VentaDb db) => _db = db;

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() =>
            Ok(await _db.Productos.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _db.Productos.FindAsync(id);
            if (producto == null) return NotFound("Producto no encontrado");
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _db.Productos.Add(producto);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest("Id no coincide");
            var existing = await _db.Productos.FindAsync(id);
            if (existing == null) return NotFound("Producto no encontrado");
            existing.Nombre = producto.Nombre;
            existing.Precio = producto.Precio;
            existing.Stock = producto.Stock;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _db.Productos.FindAsync(id);
            if (producto == null) return NotFound("Producto no encontrado");
            _db.Productos.Remove(producto);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
