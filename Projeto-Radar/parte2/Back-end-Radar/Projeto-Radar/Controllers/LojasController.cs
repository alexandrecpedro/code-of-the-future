using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.Context;
using Projeto_Radar.Entitys;

namespace Projeto_Radar.Controllers
{
    [Route("lojas")]
    [ApiController]
    public class LojasController : ControllerBase
    {

        private readonly DBContext _context;

        public LojasController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Loja>>> GetLojas()
        {
            if (_context.Lojas == null)
            {
                return NotFound();
            }

            var lojas = await _context.Lojas.ToListAsync();
            return StatusCode(200, lojas);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Loja>> GetLoja(int id)
        {
            if (_context.Lojas == null)
            {
                return NotFound();
            }
            var loja = await _context.Lojas.FindAsync(id);

            if (loja == null)
            {
                return NotFound();
            }

            return StatusCode(200, loja);
        }

        [HttpPost]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Loja>> PostLoja(Loja loja)
        {
            if (_context.Lojas == null)
            {
                return Problem("Entity set 'DBContext.Lojas'  is null.");
            }
            _context.Lojas.Add(loja);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoja", new { id = loja.Id }, loja);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutLoja(int id, Loja loja)
        {
            if (id != loja.Id)
            {
                return BadRequest();
            }

            _context.Entry(loja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LojaExists(id))
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteLoja(int id)
        {
            if (_context.Lojas == null)
            {
                return NotFound();
            }
            var loja = await _context.Lojas.FindAsync(id);
            if (loja == null)
            {
                return NotFound();
            }

            _context.Lojas.Remove(loja);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool LojaExists(int id)
        {
            return (_context.Lojas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
