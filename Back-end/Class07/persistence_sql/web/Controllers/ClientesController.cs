using Business.Entidades;
using Business.Servicos;
using Database.MySQL.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers;

public class ClientesController : Controller
{
    private static ClienteServico clienteServico = new ClienteServico(new RepositorioMySQL<Cliente>());
    public IActionResult Index()
    {
        ViewBag.clientes = clienteServico.Todos();
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

        clienteServico.Salvar(cliente);

        return Redirect("/clientes");
    }

    [Route("/clientes/{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        ViewBag.cliente = clienteServico.BuscaPorId(id);
        return View();
    }

    [Route("/clientes/{id}/atualizar")]
    public IActionResult Atualizar([FromRoute] int id, [FromForm] Cliente cliente)
    {
        cliente.Id = id;
        clienteServico.Salvar(cliente);
        return Redirect("/clientes");
    }

    [Route("/clientes/{id}/deletar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        clienteServico.ApagaPorId(id);
        return Redirect("/clientes");
    }
}
