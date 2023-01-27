using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Radar.AuthService;
using Projeto_Radar.Context;
using Projeto_Radar.Dtos;
using Projeto_Radar.Services;

namespace Projeto_Radar.Controllers
{

    public class LoginController : ControllerBase
    {
        private readonly DBContext _context;

        public LoginController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioDto usuarioDto)
        {
            if (string.IsNullOrEmpty(usuarioDto.Email) || string.IsNullOrEmpty(usuarioDto.Senha))
                return BadRequest("Prenca o email e a senha");
            Console.WriteLine(usuarioDto.Email);
            Console.WriteLine(usuarioDto.Senha);

            usuarioDto.Senha = AuthTokenService.CriptografiaSenha(usuarioDto.Senha);
            var usuario = await _context.Usuarios.Where(e => e.Email == usuarioDto.Email).FirstOrDefaultAsync();

            if (usuario is null) return NotFound("Usuario não encontrado");

            if (usuario.Senha != usuarioDto.Senha)
            {
                return BadRequest("Usuario ou senha incorreta");
            }
            var usuarioLogado = BuilderService<UsuarioLogadoDto>.Builder(usuario);
            usuarioLogado.Token = AuthTokenService.Builder(usuarioLogado);
            return Ok(usuarioLogado);

        }
    }
}
