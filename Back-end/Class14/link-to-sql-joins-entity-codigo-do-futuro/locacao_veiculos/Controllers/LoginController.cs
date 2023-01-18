using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using locacao_veiculos.Database;
using locacao_veiculos.Models;
using locacao_veiculos.DTOs;
using Microsoft.Net.Http.Headers;
using System.Collections.Specialized;
using locacao_veiculos.Servicos;

namespace locacao_veiculos.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("/login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/logar")]
        public IActionResult Logar([FromForm] LoginDTO loginDTO)
        {
            if(string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                ViewBag.erro = "Login e senha são obrigatórios";
                return View("Index");
            }


            if(!LoginService.Logar(loginDTO.Email, loginDTO.Password))
            {
                ViewBag.erro = "Usuário ou senha inválidos";
                return View("Index");
            }

            this.HttpContext.Response.Cookies.Append("admin-codigo-do-futuro", "1", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                HttpOnly = true,
            });


            return Redirect("/clientes");
        }

        [HttpGet("/sair")]
        public IActionResult Sair()
        {
            this.HttpContext.Response.Cookies.Delete("admin-codigo-do-futuro");
            return Redirect("/");
        }
    }
}
