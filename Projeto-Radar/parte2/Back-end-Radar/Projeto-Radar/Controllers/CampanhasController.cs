using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.Context;
using Projeto_Radar.Entitys;

namespace Projeto_Radar.Controllers
{
    [Route("campanhas")]
    [ApiController]
    public class CampanhasController : ControllerBase
    {
        private readonly DBContext _context;

        public CampanhasController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Campanha>>> GetCampanhas()
        {
            if (_context.Campanhas == null)
            {
                return NotFound();
            }
            var campanhas = await _context.Campanhas.ToListAsync();

            return StatusCode(200, campanhas);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Campanha>> GetCampanha(int id)
        {
            if (_context.Campanhas == null)
            {
                return NotFound();
            }
            var campanha = await _context.Campanhas.FindAsync(id);

            if (campanha == null)
            {
                return NotFound();
            }

            return StatusCode(200, campanha);

        }


        [HttpPost]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<Campanha>> PostCampanha(Campanha campanha)
        {
            if (_context.Campanhas == null)
            {
                return Problem("Banco de dados esta vazio");
            }
            campanha.Data = DateOnly.FromDateTime(DateTime.Now);
            _context.Campanhas.Add(campanha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampanha", new { id = campanha.Id }, campanha);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutCampanha(int id, Campanha campanha)
        {
            if (id != campanha.Id)
            {
                return BadRequest();
            }

            _context.Entry(campanha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampanhaExists(id))
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
        [HttpGet("/campanhasLast")]
        [Authorize(Roles = "adm,editor")]
        public async Task<ActionResult<IEnumerable<Campanha>>> GetLast()
        {
            var campanha = await _context.Campanhas.OrderByDescending(p => p.Id).FirstOrDefaultAsync();

            if (_context.Pedidos == null)
            {
                return NotFound();
            }

            return StatusCode(200, campanha);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteCampanha(int id)
        {
            if (_context.Campanhas == null)
            {
                return NotFound();
            }
            var campanha = await _context.Campanhas.FindAsync(id);
            if (campanha == null)
            {
                return NotFound();
            }

            _context.Campanhas.Remove(campanha);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CampanhaExists(int id)
        {
            return (_context.Campanhas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
