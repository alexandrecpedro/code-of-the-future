using Microsoft.AspNetCore.Mvc;
using api.ModelViews;
using api.Models;
using api.Repositorios.Interfaces;
using api.DTOs;
using api.Servicos;
using api.Servicos.Autenticacao;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

public class LoginController : ControllerBase
{
    private IServicoAdm<Administrador> _servico;
    public LoginController(IServicoAdm<Administrador> servico)
    {
        _servico = servico;
    }
    // GET: Clientes
    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AdministradorDTO administradorDTO)
    {
        if(string.IsNullOrEmpty(administradorDTO.Email) || string.IsNullOrEmpty(administradorDTO.Senha))
            return StatusCode(400, new {
                Mensagem = "Preencha o email e a senha"
            });

        var administrador = await _servico.Login(administradorDTO.Email, administradorDTO.Senha);
        if(administrador is null)
            return StatusCode(404, new {
                Mensagem = "Usuario ou senha não encontrado em nossa base de dados"
            });

        var administradorLogado = BuilderServico<AdministradorLogado>.Builder(administrador);
        administradorLogado.Token = TokenJWT.Builder(administradorLogado);

        return StatusCode(200, administradorLogado);
    }
}
