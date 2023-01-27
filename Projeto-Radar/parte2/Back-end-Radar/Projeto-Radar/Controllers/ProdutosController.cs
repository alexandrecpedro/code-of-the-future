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
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private DBContext _context;
        public ProdutosController(DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Produto>> GetProdutos()
        {
            if (_context.Produtos == null) return NotFound("Produtos não encontrados");

            var listaProdutos = await _context.Produtos.Include(c => c.Categoria).ToListAsync();

            return StatusCode(200, listaProdutos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            if (id < 1) return NotFound("Produto não encontrado, id precisa ser maior que 0");

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound("Produtos não encontrados");

            return StatusCode(200, produto);
        }

        [HttpGet("produtosLast")]
        public async Task<ActionResult<Produto>> GetLast()
        {
            var produto = await _context.Produtos
                .OrderByDescending(l => l.Id)
                .FirstOrDefaultAsync();
            if (produto.Id == null) return NotFound("Produto não encontrado");

            return StatusCode(200, produto);
        }

        [HttpPost]
        [Authorize(Roles = "adm,editor")]
        public async Task<IActionResult> PostProduto(ProdutoDto produtoDto)
        {
            var produto = BuilderService<Produto>.Builder(produtoDto);
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutoById", new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutProduto(int id, ProdutoDto produtoDto)
        {
            var produto = BuilderService<Produto>.Builder(produtoDto);

            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(200, produto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            if (_context.Produtos == null)
            {
                return NotFound();
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
