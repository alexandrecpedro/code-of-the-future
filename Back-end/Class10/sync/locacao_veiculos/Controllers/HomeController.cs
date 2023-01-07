using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using locacao_veiculos.Models;

namespace locacao_veiculos.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
