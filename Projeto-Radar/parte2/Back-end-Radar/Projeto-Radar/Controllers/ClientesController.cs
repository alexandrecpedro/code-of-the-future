using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.Context;
using Projeto_Radar.Entitys;


namespace Projeto_Radar.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DBContext _context;

        public ClientesController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            return await _context.Clientes.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("clientesLast")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesLast()
        {
            var cliente = await _context.Clientes.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            return StatusCode(200,cliente);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutCliente([FromRoute] int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                cliente.Id = id;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'DBContext.Clientes'  is null.");
            }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

