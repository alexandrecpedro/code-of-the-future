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
    [Route("posicoesProdutos")]
    [ApiController]
    public class PosicoesProdutosController : ControllerBase
    {
        private readonly DBContext _context;

        public PosicoesProdutosController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<PosicoesProduto>>> GetPosicoesProdutos()
        {
            if (_context.PosicoesProdutos == null)
            {
                return NotFound();
            }
            var posicoesProdutos = await _context.PosicoesProdutos.ToListAsync();
            return StatusCode(200, posicoesProdutos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<PosicoesProduto>> GetPosicoesProduto(int id)
        {
            if (_context.PosicoesProdutos == null)
            {
                return NotFound();
            }
            var posicoesProduto = await _context.PosicoesProdutos.FindAsync(id);

            if (posicoesProduto == null)
            {
                return NotFound();
            }

            return StatusCode(200, posicoesProduto);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutPosicoesProduto(int id, PosicoesProduto posicoesProduto)
        {
            if (id != posicoesProduto.Id)
            {
                return BadRequest();
            }

            _context.Entry(posicoesProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosicoesProdutoExists(id))
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
        public async Task<ActionResult<PosicoesProduto>> PostPosicoesProduto(PosicoesProdutoDto posicoesProdutoDto)
        {
            var posicoesProduto = BuilderService<PosicoesProduto>.Builder(posicoesProdutoDto);

            if (_context.PosicoesProdutos == null)
            {
                return Problem("Banco de dados está ");
            }
            _context.PosicoesProdutos.Add(posicoesProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosicoesProduto", new { id = posicoesProduto.Id }, posicoesProduto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeletePosicoesProduto(int id)
        {
            if (_context.PosicoesProdutos == null)
            {
                return NotFound();
            }
            var posicoesProduto = await _context.PosicoesProdutos.FindAsync(id);
            if (posicoesProduto == null)
            {
                return NotFound();
            }

            _context.PosicoesProdutos.Remove(posicoesProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PosicoesProdutoExists(int id)
        {
            return (_context.PosicoesProdutos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
