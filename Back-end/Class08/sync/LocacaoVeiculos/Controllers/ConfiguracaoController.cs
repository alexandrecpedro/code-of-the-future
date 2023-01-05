using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocacaoVeiculos.Context;
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Controllers
{
    public class ConfiguracaoController : Controller
    {
        private readonly DbContexto _context;

        public ConfiguracaoController(DbContexto context)
        {
            _context = context;
        }

        // GET: Configuracao
        public async Task<IActionResult> Index()
        {
              return _context.Configuracoes != null ? 
                          View(await _context.Configuracoes.ToListAsync()) :
                          Problem("Entity set 'DbContexto.Configuracoes'  is null.");
        }

        // GET: Configuracao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Configuracoes == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // GET: Configuracao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuracao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiasDeLocacao")] Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracao);
        }

        // GET: Configuracao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Configuracoes == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracoes.FindAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }
            return View(configuracao);
        }

        // POST: Configuracao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiasDeLocacao")] Configuracao configuracao)
        {
            if (id != configuracao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracaoExists(configuracao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(configuracao);
        }

        // GET: Configuracao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Configuracoes == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // POST: Configuracao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Configuracoes == null)
            {
                return Problem("Entity set 'DbContexto.Configuracoes'  is null.");
            }
            var configuracao = await _context.Configuracoes.FindAsync(id);
            if (configuracao != null)
            {
                _context.Configuracoes.Remove(configuracao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracaoExists(int id)
        {
          return (_context.Configuracoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
