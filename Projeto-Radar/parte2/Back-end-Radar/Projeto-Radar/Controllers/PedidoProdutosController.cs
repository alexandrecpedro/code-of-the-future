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
    [Route("pedidosProdutos")]
    [ApiController]
    public class PedidoProdutosController : ControllerBase
    {

        private readonly DBContext _context;

        public PedidoProdutosController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<PedidoProduto>>> GetPedidoProdutos()
        {
            if (_context.PedidoProdutos == null)
            {
                return NotFound();
            }
            var pedidoProdutos = await _context.PedidoProdutos.Include(p => p.Produto).Include(pe => pe.Pedido).ToListAsync();
            return StatusCode(200, pedidoProdutos);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<PedidoProduto>> GetPedidoProduto(int id)
        {
            if (_context.PedidoProdutos == null)
            {
                return NotFound();
            }
            var pedidoProduto = await _context.PedidoProdutos.FindAsync(id);

            if (pedidoProduto == null)
            {
                return NotFound();
            }

            return StatusCode(200, pedidoProduto);

        }


        [HttpPut("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<IActionResult> PutPedidoProduto(int id, PedidoProduto pedidoProduto)
        {
            if (id != pedidoProduto.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoProdutoExists(id))
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
        public async Task<ActionResult<PedidoProduto>> PostPedidoProduto(PedidoProdutoDto pedidoProdutoDto)
        {
            var pedidoProduto = BuilderService<PedidoProduto>.Builder(pedidoProdutoDto);
            if (_context.PedidoProdutos == null)
            {
                return Problem("Entity set 'DBContext.PedidoProdutos'  is null.");
            }
            _context.PedidoProdutos.Add(pedidoProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoProduto", new { id = pedidoProduto.Id }, pedidoProduto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<IActionResult> DeletePedidoProduto(int id)
        {
            if (_context.PedidoProdutos == null)
            {
                return NotFound();
            }
            var pedidoProduto = await _context.PedidoProdutos.FindAsync(id);
            if (pedidoProduto == null)
            {
                return NotFound();
            }

            _context.PedidoProdutos.Remove(pedidoProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoProdutoExists(int id)
        {
            return (_context.PedidoProdutos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
