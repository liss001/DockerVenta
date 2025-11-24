using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DockerVenta.Data;
using DockerVenta.Models;

namespace DockerVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly VentaDb _db;
        public ClienteController(VentaDb db) => _db = db;

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes() =>
            Ok(await _db.Clientes.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null) return NotFound("Cliente no encontrado");
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest("Id no coincide");
            var existing = await _db.Clientes.FindAsync(id);
            if (existing == null) return NotFound("Cliente no encontrado");
            existing.Nombre = cliente.Nombre;
            existing.Telefono = cliente.Telefono;
            existing.Email = cliente.Email;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null) return NotFound("Cliente no encontrado");
            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
