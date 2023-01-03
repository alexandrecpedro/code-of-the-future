using Entity.Context;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity.Controllers;

public class ClientesController : Controller
{
    private readonly DbContexto _context;
    public ClientesController(DbContexto context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.clientes = await _context.Clientes.ToListAsync();
        return View();
    }

    public IActionResult Novo()
    {
        return View();
    }
    
    public async Task<IActionResult> CadastrarAsync([FromForm] Cliente cliente)
    {
        if(string.IsNullOrEmpty(cliente.Nome))
        {
            ViewBag.erro = "O nome não pode ser vazio";
            return View();
        }

        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return Redirect("/clientes");
    }

    [Route("/clientes/{id}/editar")]
    public async Task<IActionResult> EditarAsync([FromRoute] int id)
    {
        var clienteDb = await _context.Clientes.Where(client => client.Id == id).FirstOrDefaultAsync();
        
        return View();
    }

    [Route("/clientes/{id}/atualizar")]
    public async Task<IActionResult> AtualizarAsync([FromRoute] int id, [FromForm] Cliente cliente)
    {
        var clienteDb = await _context.Clientes.Where(client => client.Id == id).FirstOrDefaultAsync();
        if (clienteDb is null)
            return Redirect("/clientes");
        
        clienteDb.Nome = cliente.Nome;
        clienteDb.Email = cliente.Email;
        _context.Update(clienteDb);
        await _context.SaveChangesAsync();

        return Redirect("/clientes");
    }

    [Route("/clientes/{id}/deletar")]
    public async Task<IActionResult> ApagarAsync([FromRoute] int id)
    {
        var clienteDb = await _context.Clientes.Where(client => client.Id == id).FirstOrDefaultAsync();
        if (clienteDb is null)
            return Redirect("/clientes");
        
        _context.Clientes.Remove(clienteDb);
        await _context.SaveChangesAsync();
        
        return Redirect("/clientes");
    }
}
