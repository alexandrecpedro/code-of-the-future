using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Entity.Controllers;

[Route("/")]
public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;
    private readonly DbContexto _context;

    // public HomeController(DbContexto context, ILogger<HomeController> logger)
    public HomeController(DbContexto context)
    {
        _context = context;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        // var context = new DbContexto();
        var clientes = await this._context.Clientes.Where(client => client.Nome.Contains("Dani")).ToListAsync();
        var fornecedores = await this._context.Fornecedores.Where(supplyer => supplyer.Nome.Contains("Dani")).ToListAsync();

        ViewBag.clientes = clientes;
        ViewBag.fornecedores = fornecedores;
        
        return View();
    }

    [Route("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("/error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
