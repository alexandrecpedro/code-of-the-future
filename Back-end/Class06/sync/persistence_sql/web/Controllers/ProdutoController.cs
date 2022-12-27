using Database.MySql;
using Microsoft.AspNetCore.Mvc;
using Negocio.Entidades;
using Negocio.Servicos;

namespace web.Controllers;

public class ProdutoController : Controller
{
    private static Servico<Produto> produtoServico = new Servico<Produto>(new RepositorioMYSQL<Produto>());

    public IActionResult Index()
    {
        ViewBag.produtos = produtoServico.BuscarTodos();
        return View();
    }

    public IActionResult Cadastrar([FromForm] Produto produto)
    {
        if(string.IsNullOrEmpty(produto.Nome))
        {
            ViewBag.erro = "O nome não pode ser vazio";
            return View();
        }

        if(string.IsNullOrEmpty(produto.Descricao))
        {
            ViewBag.erro = "A descrição do produto não pode ser vazia";
            return View();
        }

        produtoServico.Salvar(produto);

        return Redirect("/produtos");
    }

    [Route("/produtos/{id}/editar")]
    public IActionResult Editar([FromRoute] string id)
    {
        ViewBag.produto = produtoServico.BuscaPorId(id);
        return View();
    }

    [Route("/produtos/{id}/atualizar")]
    public IActionResult Atualizar([FromRoute] string id, [FromForm] Produto produto)
    {
        produto.Id = id;
        produtoServico.Salvar(produto);
        return Redirect("/produtos");
    }

    [Route("/produtos/{id}/deletar")]
    public IActionResult Apagar([FromRoute] string id)
    {
        produtoServico.Apagar(id);
        return Redirect("/produtos");
    }
}
