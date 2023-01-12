using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using locacao_veiculos.Database;
using locacao_veiculos.Models;
using admin_cms.Filtros;

namespace locacao_veiculos.Controllers
{
    [LogadoAttribute]
    public class PedidoCarrosController : Controller
    {
        private readonly LocacaoContext _context;

        public PedidoCarrosController(LocacaoContext context)
        {
            _context = context;
        }

        // GET: PedidoCarros
        public async Task<IActionResult> Index(int? pedidoId)
        {
            var pedidosCarros = _context.PedidoCarros;
            List<PedidoCarro> lista;
            if(pedidoId is not null) 
                lista = await pedidosCarros.Where(pc => pc.PedidoId == pedidoId).Include(p => p.Carro).Include(p => p.Pedido).ToListAsync();
            else
                lista = await pedidosCarros.Include(p => p.Carro).Include(p => p.Pedido).ToListAsync();
            return View(lista);
        }

        // GET: PedidoCarros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PedidoCarros == null)
            {
                return NotFound();
            }

            var pedidoCarro = await _context.PedidoCarros
                .Include(p => p.Carro)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoCarro == null)
            {
                return NotFound();
            }

            return View(pedidoCarro);
        }

        // GET: PedidoCarros/Create
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome");
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id");
            return View();
        }

        // POST: PedidoCarros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PedidoId,CarroId,ValorTrasacao,DataTrasacao")] PedidoCarro pedidoCarro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoCarro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome", pedidoCarro.CarroId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", pedidoCarro.PedidoId);
            return View(pedidoCarro);
        }

        // GET: PedidoCarros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PedidoCarros == null)
            {
                return NotFound();
            }

            var pedidoCarro = await _context.PedidoCarros.FindAsync(id);
            if (pedidoCarro == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome", pedidoCarro.CarroId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", pedidoCarro.PedidoId);
            return View(pedidoCarro);
        }

        // POST: PedidoCarros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PedidoId,CarroId,ValorTrasacao,DataTrasacao")] PedidoCarro pedidoCarro)
        {
            if (id != pedidoCarro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoCarro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoCarroExists(pedidoCarro.Id))
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
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome", pedidoCarro.CarroId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", pedidoCarro.PedidoId);
            return View(pedidoCarro);
        }

        // GET: PedidoCarros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PedidoCarros == null)
            {
                return NotFound();
            }

            var pedidoCarro = await _context.PedidoCarros
                .Include(p => p.Carro)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoCarro == null)
            {
                return NotFound();
            }

            return View(pedidoCarro);
        }

        // POST: PedidoCarros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PedidoCarros == null)
            {
                return Problem("Entity set 'LocacaoContext.PedidoCarros'  is null.");
            }
            var pedidoCarro = await _context.PedidoCarros.FindAsync(id);
            if (pedidoCarro != null)
            {
                _context.PedidoCarros.Remove(pedidoCarro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoCarroExists(int id)
        {
          return (_context.PedidoCarros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
