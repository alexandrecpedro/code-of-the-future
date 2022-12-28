using System.Diagnostics;
using database.mysql;
using Microsoft.AspNetCore.Mvc;
using negocio.models;
using negocio.services;
using web.Models;

namespace web.Controllers;

public class HomeController : Controller
{

    ProdutoServico produtoServico = new ProdutoServico(new RepositorioMYSQL<Produto>());
    
    public IActionResult Index()
    {
        var listaDeProdutos = produtoServico.BuscarTodos();
        int? quantidadeDeProdutos = listaDeProdutos.Count();
        int quantidade = 0;
        foreach (Produto produto in listaDeProdutos)
        {
            quantidade += produto.Quantidade;
        }
        int? quantidadeDeItensNoEstoque = quantidade;

        var dataAtual =  DateTime.Now;
        var data3dias = dataAtual.AddDays(3);
        int produtosVencidos=0;
        int produtosAVencerEm3Dias=0;
         foreach (Produto produto in listaDeProdutos)
        {
            if(produto.data_vencimento < dataAtual)
            {
                produtosVencidos +=1;
            }
            if(produto.data_vencimento > dataAtual && produto.data_vencimento < data3dias)
            {
                produtosAVencerEm3Dias +=1;
            }
        }
        ViewBag.produtosAVencerEm3Dias = produtosAVencerEm3Dias;
        ViewBag.produtosVencidos = produtosVencidos;
        ViewBag.quantidadeDeProdutos = quantidadeDeProdutos;
        ViewBag.quantidadeDeItensNoEstoque = quantidadeDeItensNoEstoque;
        return View();
    }

    [Route("/produtos/produtos-vencidos")]
    public IActionResult ProdutosVencidos()
    {
        var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
        //Console.WriteLine(dataAtual);
        var produtosVencidos = produtoServico.BuscarTodos($"data_vencimento<'{dataAtual}';");
        ViewBag.produtosVencidos = produtosVencidos;
        return View();
    }

}
