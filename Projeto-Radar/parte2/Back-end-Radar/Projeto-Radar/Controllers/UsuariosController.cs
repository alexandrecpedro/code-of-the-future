using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.AuthService;
using Projeto_Radar.Context;
using Projeto_Radar.Entitys;

namespace Projeto_Radar.Controllers
{
    [Route("/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DBContext _context;

        public UsuariosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DBContext.Usuarios'  is null.");
            }
            usuario.Senha = AuthTokenService.CriptografiaSenha(usuario.Senha);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
