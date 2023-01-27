using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.Context;
using Projeto_Radar.Dtos;
using Projeto_Radar.Entitys;
using Projeto_Radar.Services;

namespace Projeto_Radar.Controllers
{
    [Route("pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly DBContext _context;

        public PedidosController(DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedidos = await _context.Pedidos.Include(c => c.Cliente).ToListAsync();

            return StatusCode(200, pedidos);
        }

        [HttpGet("/pedidosLast")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetLast()
        {
            var pedido = await _context.Pedidos.OrderByDescending(p => p.Id).FirstOrDefaultAsync();

            if (_context.Pedidos == null)
            {
                return NotFound();
            }

            return StatusCode(200, pedido);
        }

        [HttpGet("/pedidosLastList")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosLast()
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedidos = await _context.Pedidos.OrderByDescending(p => p.Id).Include(c => c.Cliente).ToListAsync();

            return StatusCode(200, pedidos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return StatusCode(200, pedido);
        }

        [HttpGet("/{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Pedido>> GetPorProdutoId(int id)
        {
           if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return StatusCode(200, pedido);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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
        public async Task<ActionResult<Pedido>> PostPedido(PedidoDto pedidodto)
        {
            var pedido = BuilderService<Pedido>.Builder(pedidodto);
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'DBContext.Pedidos'  is null.");
            }
            pedido.Data = DateOnly.FromDateTime(DateTime.Now);
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
