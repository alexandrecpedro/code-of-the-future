using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class LivrosController : Controller
{
    // ex 1: /livros
    // ex 2: /livros/143978
    [Route("/livros/{isbn?}")]
    public ActionResult Get(string? isbn)
    {
        if (!string.IsNullOrEmpty(isbn))
        {
            return Content($"Livro específico: {isbn}");
        }
        return Content("Todos os livros");
    }

    // ex 1: /livros/idioma
    // ex 2: /livros/idioma/en
    // ex 3: /livros/idioma/de
    [Route("/livros/idioma/{idioma=ptBR}")]
    public ActionResult GetByLanguage(string idioma)
    {
        return Content($"Todos os livros no idioma: {idioma}");
    }

    // ex: /livros/autor/5
    [Route("/livros/autor/{id:int}")]
    public ActionResult GetAuthorById(int id)
    {
        return Content($"Livros do autor com id = {id}");
    }

    // ex: /livros/autor/Tolkien
    [Route("/livros/autor/{nome}")]
    public ActionResult GetAuthorByName(string nome)
    {
        return Content($"Livros do autor com nome = {nome}");
    }
}