
using database.mysql;
using Microsoft.AspNetCore.Mvc;
using negocio.models;
using negocio.services;


namespace web.Controllers;

public class ProdutosController : Controller
{
    ProdutoServico produtoServico = new ProdutoServico(new RepositorioMYSQL<Produto>());
    public IActionResult Index()
    {
        ViewBag.produtos = produtoServico.BuscarTodos();
        return View();
    }
    public IActionResult Cadastrar([FromForm] Produto produto)
    {
        if( string.IsNullOrEmpty(produto.Nome) )
        {
            ViewBag.erro = "O nome não pode ser vazio!";
            return View();
        }
        
        produtoServico.Salvar(produto);
        return Redirect("/produtos");
    }
    
    [Route("/produtos/{id}/atualizar")]
    public IActionResult Atualizar([FromRoute] int id,[FromForm] Produto produto)
    {
        produto.Id = id;
        produtoServico.Salvar(produto);
        return Redirect("/produtos");
    }
    
    [Route("/produtos/{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var produto = produtoServico.BuscarPorId(id);
        ViewBag.produto = produto;
        return View();
    }
    
     [Route("/produtos/{id}/deletar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        produtoServico.ApagarPorId(id);
        return Redirect("/produtos");
    }

    public IActionResult Novo()
    {
        return View();
    }

}
