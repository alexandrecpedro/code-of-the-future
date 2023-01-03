using Entity.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity.Controllers;

public class GenericController<TEntity> : Controller where TEntity : class
{
    private readonly DbContexto _context;
    private DbSet<TEntity> _dbSet;
    
    public GenericController(DbContexto context)
    {
        this._context = context;
        this._dbSet = _context.Set<TEntity>();
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.fornecedores = await _dbSet.ToListAsync();
        return View();
    }

    public IActionResult Novo()
    {
        return View();
    }

    public async Task<IActionResult> CadastrarAsync([FromForm] TEntity entity)
    {
        if(string.IsNullOrEmpty(entity.ToString()))
        {
            ViewBag.erro = $"{entity} não pode ser vazio";
            return View();
        }

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return Redirect("/fornecedores");
    }

    [Route("/fornecedores/{id}/editar")]
    public async Task<IActionResult> EditarAsync([FromRoute] int id)
    {
        var entityDb = await _dbSet.FindAsync(id);
        
        return View();
    }

    [Route("/fornecedores/{id}/atualizar")]
    public async Task<IActionResult> AtualizarAsync([FromRoute] int id, [FromForm] TEntity entity)
    {
        var entityDb = await _dbSet.FindAsync(id);
        if (entityDb is null)
            return Redirect("/fornecedores");
        
        entityDb = entity;
        _dbSet.Update(entityDb);
        await _context.SaveChangesAsync();

        return Redirect("/fornecedores");
    }

    [Route("/fornecedores/{id}/deletar")]
    public async Task<IActionResult> ApagarAsync([FromRoute] int id)
    {
        // var entityDb = await _dbSet.FindAsync(id);
        var entityDb = await _dbSet.FindAsync(id);
        if (entityDb is null)
            return Redirect("/fornecedores");
        
        _dbSet.Remove(entityDb);
        await _context.SaveChangesAsync();
        
        return Redirect("/fornecedores");
    }
}
