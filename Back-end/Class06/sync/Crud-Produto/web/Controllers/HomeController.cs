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
    
    List<Produto> listaDeProdutos = new List<Produto>();
    List<Produto> listaDeProdutosVencidos = new List<Produto>();
    List<Produto> listaDeProdutosAVencer = new List<Produto>();
    int quantidade = 0;
    int? quantidadeDeProdutos=0;
    int? quantidadeDeItensNoEstoque = 0;
    static DateTime dataAtualDT =  DateTime.Now;
    DateTime data3dias = dataAtualDT.AddDays(3);
    int produtosVencidos=0;
    int produtosAVencerEm3Dias=0;

    public IActionResult Index()
    {
        listaDeProdutos = produtoServico.BuscarTodos();
        quantidadeDeProdutos = listaDeProdutos.Count();
        foreach (Produto produto in listaDeProdutos)
        {
            quantidade += produto.Quantidade;
        }
        
        quantidadeDeItensNoEstoque = quantidade;

        
         foreach (Produto produto in listaDeProdutos)
        {
            if(produto.Data_vencimento < dataAtualDT)
            {
                produtosVencidos +=1;
            }
            if(produto.Data_vencimento > dataAtualDT && produto.Data_vencimento < data3dias)
            {
                produtosAVencerEm3Dias +=1;
                listaDeProdutosAVencer.Add(produto);
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
        string dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
        listaDeProdutosVencidos = produtoServico.BuscarTodos($"data_vencimento<'{dataAtual}';");
        var listadeProdutosVencidosOrdenada = listaDeProdutosVencidos.OrderByDescending(produto => produto.Data_vencimento).Reverse();
        ViewBag.listaDeProdutosVencidos = listadeProdutosVencidosOrdenada;
        return View();
    }
    
    [Route("/produtos/produtos-a-vencer")]
    public IActionResult ProdutosAVencer()
    {
        listaDeProdutos = produtoServico.BuscarTodos();
        foreach (Produto produto in listaDeProdutos)
        {
            if(produto.Data_vencimento > dataAtualDT && produto.Data_vencimento < data3dias)
                {
                    listaDeProdutosAVencer.Add(produto);
                }           
        }

        var listaDeProdutosAVencerOrdenada = listaDeProdutosAVencer.OrderByDescending(produto => produto.Data_vencimento).Reverse();
        ViewBag.listaDeProdutosAVencer = listaDeProdutosAVencerOrdenada;
        return View();
    }
    
    [Route("/produtos/produtos-em-estoque")]
    public IActionResult ProdutosEmEstoque()
    {
        listaDeProdutos = produtoServico.BuscarTodos();
        var listaOrdenada = listaDeProdutos.OrderByDescending(x => x.Quantidade).ToList();
        ViewBag.listaDeProdutosAVencer = listaOrdenada;
        return View();
    }

}
