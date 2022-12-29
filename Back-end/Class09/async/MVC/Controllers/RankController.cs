using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers;

public class RankController : Controller
{
    // ViewResult - represents HTML
    public ActionResult Index()
    {
        // ViewBag.Id = 8;
        // ViewBag.Avatar = "👩‍🦰";
        // ViewBag.PlayerName = "Marlene F. Martelli";
        // ViewBag.Points = 1298;

        var modelo = RankingService.Instance.GetAll;

        return View(modelo);
    }

    [HttpGet]
    public ActionResult NovoScore()
    {
        var modelo = new NewScoreViewModel();
        return View(modelo);
    }

    [HttpPost]
    public ActionResult NovoScore(NewScoreViewModel input)
    {
        if (ModelState.IsValid)
        {
            RankingService.Instance.Create(input.NewScore);
            return Redirect("/rank");
        }
        return NovoScore();
    }

    // EmptyResult - it does not represent any result
    public ActionResult Vazio()
    {
        return new EmptyResult();
    }

    // RedirectResult - redirects to other URL
    public ActionResult VaiPraHome()
    {
        return Redirect("/Home/Index");
    }

    // JsonResult - object in JSON notation
    public ActionResult Json()
    {
        var obj = new { id = 1234, nome = "Fulano de Tal" };
        return Json(obj);
    }

    // ContentResult - text result
    public ActionResult OlaMundo()
    {
        return Content("Olá, Mundo!");
    }

    // FileContentResult - file to download
    public ActionResult Logo()
    {
        var filePath = Environment.GetEnvironmentVariable("GET_FILE")!;
        // var filePath = Server.MapPath("~/Images/dotnet6.png");
        var contentType = "image/png";
        // var contentType = "data:image/png;base64,{0}";
        return File(filePath, contentType, "logo.png");
    }
}