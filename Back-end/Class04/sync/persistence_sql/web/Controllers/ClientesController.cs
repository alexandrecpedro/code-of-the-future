using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Business.Models;

namespace web.Controllers;

public class ClientesController : Controller
{
    public IActionResult Index()
    {
        ViewBag.clientes = Cliente.Todos();
        return View();
    }

    public IActionResult Novo()
    {
        return View();
    }

    public IActionResult Cadastrar([FromForm] Cliente cliente)
    {
        if(string.IsNullOrEmpty(cliente.Nome))
        {
            ViewBag.erro = "O nome não pode ser vazio";
            return View();
        }

        cliente.Salvar();

        return Redirect("/clientes");
    }

    [Route("/clientes/{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        ViewBag.cliente = Cliente.BuscaPorId(id);
        return View();
    }

    [Route("/clientes/{id}/atualizar")]
    public IActionResult Atualizar([FromRoute] int id, [FromForm] Cliente cliente)
    {
        cliente.Id = id;
        cliente.Salvar();
        return Redirect("/clientes");
    }

    [Route("/clientes/{id}/deletar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        Cliente.ApagaPorId(id);
        return Redirect("/clientes");
    }
}
