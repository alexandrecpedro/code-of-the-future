
using database.mysql;
using Microsoft.AspNetCore.Mvc;
using negocio.models;
using negocio.services;


namespace web.Controllers;

public class ProdutosController : Controller
{
    ProdutoServico produtoServico = new ProdutoServico(new RepositorioMYSQL<Produto>());

    List<Produto> listaDeProduto = new List<Produto>();

    public IActionResult Index()
    {
        listaDeProduto = produtoServico.BuscarTodos();
        var listaDeProdutoOrdenada = listaDeProduto.OrderByDescending(produto => produto.Nome).Reverse();
        ViewBag.produtos = listaDeProdutoOrdenada;
        return View();
    }
    public IActionResult Cadastrar([FromForm] Produto produto)
    {
        if( string.IsNullOrEmpty(produto.Nome) || string.IsNullOrEmpty(produto.Descricao ) || string.IsNullOrEmpty(produto.Data_vencimento.ToString()))
        {
            ViewBag.erro = "Preencha todos os campos para cadastrar um produto!";
            return View();
        }
        
        produtoServico.Salvar(produto);
        return Redirect("/produtos");
    }
    
    [Route("/produtos/{id}/atualizar")]
    public IActionResult Atualizar([FromRoute] int id,[FromForm] Produto produto)
    {
        produto.Id = id;        
        var dataConvertida = Convert.ToDateTime(produto.Data_vencimento).ToString("yyyy-MM-dd HH:MM:ss");
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
