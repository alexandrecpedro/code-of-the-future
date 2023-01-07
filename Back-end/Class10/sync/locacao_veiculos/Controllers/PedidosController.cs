using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using locacao_veiculos.Database;
using locacao_veiculos.Models;
using locacao_veiculos.ModelViews;

namespace locacao_veiculos.Controllers
{
    public class PedidosController : Controller
    {
        private readonly LocacaoContext _context;

        public PedidosController(LocacaoContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            /*
            // query força bruta
            var pedidos = new List<PedidoResumido>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = """
                    SELECT 
                        p.Id,
                        c.Nome AS NomeCliente,
                        c0.Nome AS NomeCarro,
                        m.Nome AS MarcaDoCarro,
                        p.DataLocacao AS DataLocacaoPedido,
                        p.DataEntrega AS DataEntregaPedido
                    FROM Pedidos AS p
                    INNER JOIN Clientes AS c ON p.ClienteId = c.Id
                    INNER JOIN Carros AS c0 ON p.CarroId = c0.Id
                    INNER JOIN Marcas AS m ON c0.MarcaId = m.Id
                """;

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        pedidos.Add(new PedidoResumido{
                            PedidoId = Convert.ToInt32(result["Id"]),
                            NomeCliente = result["NomeCliente"]?.ToString(),
                            MarcaDoCarro = result["MarcaDoCarro"]?.ToString(),
                            DataEntregaPedido = Convert.ToDateTime(result["DataEntregaPedido"]),
                            DataLocacaoPedido = Convert.ToDateTime(result["DataLocacaoPedido"]),
                        });
                    }
                }
            }
            */
            /*
            // query força bruta encapsulado

            object[] parameters =  { 1 };

            var query = $@"""
                    SELECT 
                        p.Id,
                        c.Nome AS NomeCliente,
                        c0.Nome AS NomeCarro,
                        m.Nome AS MarcaDoCarro,
                        p.DataLocacao AS DataLocacaoPedido,
                        p.DataEntrega AS DataEntregaPedido
                    FROM Pedidos AS p
                    INNER JOIN Clientes AS c ON p.ClienteId = c.Id
                    INNER JOIN Carros AS c0 ON p.CarroId = c0.Id
                    INNER JOIN Marcas AS m ON c0.MarcaId = m.Id
                    where p.Id={0}
            """;
            var pedidos2 = new SqlQueryFromRaw(_context).SqlQueryRaw(query, parameters);
            */
            
            // ==== link to sql
            var pedidos =  await Task.FromResult(
                from ped in _context.Pedidos
                join cli in _context.Clientes on ped.ClienteId equals cli.Id
                join car in _context.Carros on ped.CarroId equals car.Id
                join mod in _context.Modelos on car.ModeloId equals mod.Id
                join mar in _context.Marcas on mod.MarcaId equals mar.Id
                select new PedidoResumido {
                    PedidoId = ped.Id,
                    NomeCliente = cli.Nome,
                    NomeCarro = car.Nome,
                    ModeloDoCarro = mod.Nome,
                    MarcaDoCarro = mar.Nome,
                    DataLocacaoPedido = ped.DataLocacao,
                    DataEntregaPedido = ped.DataEntrega
                }
            );
            


            /*
            //===== Join nativo do entity
            var pedidos = await _context.Pedidos.Join(
                _context.Clientes,
                ped => ped.ClienteId,
                cli => cli.Id,
                (ped, cli) => new {
                    PedidoId = ped.Id,
                    DataLocacaoPedido = ped.DataLocacao,
                    DataEntregaPedido = ped.DataEntrega,
                    NomeCliente = cli.Nome,
                    CarroId = ped.CarroId
                }
            ).Join(
                _context.Carros,
                pedCli => pedCli.CarroId,
                carro => carro.Id,
                (pedCli, carro) => new {
                    PedidoId = pedCli.PedidoId,
                    NomeCliente = pedCli.NomeCliente,
                    NomeCarro = carro.Nome,
                    MarcaId = carro.MarcaId,
                    DataLocacaoPedido = pedCli.DataLocacaoPedido,
                    DataEntregaPedido = pedCli.DataEntregaPedido
                }
            ).Join(
                _context.Marcas,
                pedCliCarr => pedCliCarr.MarcaId,
                marca => marca.Id,
                (pedCliCarr, marca) => new PedidoResumido {
                    PedidoId = pedCliCarr.PedidoId,
                    NomeCliente = pedCliCarr.NomeCliente,
                    NomeCarro = pedCliCarr.NomeCarro,
                    MarcaDoCarro = marca.Nome,
                    DataLocacaoPedido = pedCliCarr.DataLocacaoPedido,
                    DataEntregaPedido = pedCliCarr.DataEntregaPedido
                }
            ).ToListAsync();
            */

            ViewBag.pedidos = pedidos;
            return View();
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Carro)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,CarroId,DataLocacao")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                var config = _context.Configuracoes.FirstOrDefault();
                var dias = config is not null ? config.DiasDeLocacao : 1;
                pedido.DataEntrega = pedido.DataLocacao.AddDays(dias);
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome", pedido.CarroId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome", pedido.CarroId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,CarroId,DataLocacao,DataEntrega")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Nome", pedido.CarroId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Carro)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'LocacaoContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
