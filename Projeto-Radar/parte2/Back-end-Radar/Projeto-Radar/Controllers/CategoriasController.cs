using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.Context;
using Projeto_Radar.Entitys;

namespace Projeto_Radar.Controllers
{
    [Route("categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly DBContext _context;

        public CategoriasController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categorias = await _context.Categorias.ToListAsync();

            return StatusCode(200, categorias);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return StatusCode(200, categoria);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'DBContext.Categorias'  is null.");
            }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return (_context.Categorias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
