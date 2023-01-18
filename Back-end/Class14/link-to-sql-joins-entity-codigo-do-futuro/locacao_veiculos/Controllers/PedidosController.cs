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
using admin_cms.Filtros;

namespace locacao_veiculos.Controllers
{
    [LogadoAttribute]
    public class PedidosController : Controller
    {
        private readonly LocacaoContext _context;

        public PedidosController(LocacaoContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index(int page = 1)
        {
            var take = 5;
            var skip = take * (page - 1);

             var marcas =  await Task.FromResult(
                (
                    from marca in _context.Marcas
                    join modelo in _context.Modelos on marca.Id equals modelo.MarcaId into MarcaModeloLeft
                    from subMarcaModelo in MarcaModeloLeft.DefaultIfEmpty()
                    where marca.Nome.Contains("m")
                    select new {
                        Id = marca.Id,
                        Nome = marca.Nome
                    }
                ).Skip(skip).Take(take) // calculado
                // ).Skip(0).Take(5) // pagina 1
                // ).Skip(5).Take(5) // pagina 2
                // ).Skip(10).Take(5) // pagina 3
            );

            Console.WriteLine("======================");

            foreach(var marca in marcas)
            {
                Console.WriteLine(marca.Nome);
            }

            Console.WriteLine("======================");

            
            // ==== link to sql
            var pedidos =  await Task.FromResult(
                (
                    from ped in _context.Pedidos
                    join cli in _context.Clientes on ped.ClienteId equals cli.Id
                    join pedCarro in _context.PedidoCarros on ped.Id equals pedCarro.PedidoId
                    join car in _context.Carros on pedCarro.CarroId equals car.Id
                    join mod in _context.Modelos on car.ModeloId equals mod.Id
                    join mar in _context.Marcas on mod.MarcaId equals mar.Id
                    group pedCarro by new {
                        Id = ped.Id,
                        Nome = cli.Nome,
                        MarcaNome = mar.Nome,
                        DataLocacao = ped.DataLocacao,
                        DataEntrega = ped.DataEntrega
                    } into agrupamento
                    where agrupamento.Sum(pedCarro => pedCarro.ValorTrasacao) > 100000
                    select new PedidoResumido {
                        PedidoId = agrupamento.Key.Id,
                        NomeCliente = agrupamento.Key.Nome,
                        MarcaDoCarro = agrupamento.Key.MarcaNome,
                        DataLocacaoPedido = agrupamento.Key.DataLocacao,
                        DataEntregaPedido = agrupamento.Key.DataEntrega,
                        ValorTotal = agrupamento.Sum(pedCarro => pedCarro.ValorTrasacao)
                    }
                )
            );


            /*
            SELECT 
                `p`.`Id` AS `PedidoId`, 
                `c`.`Nome` AS `NomeCliente`, 
                `m0`.`Nome` AS `MarcaDoCarro`, 
                `p`.`DataLocacao` AS `DataLocacaoPedido`, 
                `p`.`DataEntrega` AS `DataEntregaPedido`,
                sum(p0.ValorTrasacao) as ValorTotal
            FROM `Pedidos` AS `p`
            INNER JOIN `Clientes` AS `c` ON `p`.`ClienteId` = `c`.`Id`
            INNER JOIN `PedidoCarros` AS `p0` ON `p`.`Id` = `p0`.`PedidoId`
            INNER JOIN `Carros` AS `c0` ON `p0`.`CarroId` = `c0`.`Id`
            INNER JOIN `Modelos` AS `m` ON `c0`.`ModeloId` = `m`.`Id`
            INNER JOIN `Marcas` AS `m0` ON `m`.`MarcaId` = `m0`.`Id`
            group by 
                `p`.`Id` ,
                `c`.`Nome`,
                `m0`.`Nome`,
                `p`.`DataLocacao`,
                `p`.`DataEntrega` 
            having sum(p0.ValorTrasacao)  > 100000
            */

            /*
            // subquery
            SELECT 
                `p`.`Id` AS `PedidoId`, 
                `c`.`Nome` AS `NomeCliente`, 
                `m0`.`Nome` AS `MarcaDoCarro`, 
                `p`.`DataLocacao` AS `DataLocacaoPedido`, 
                `p`.`DataEntrega` AS `DataEntregaPedido`,
                sum(p0.ValorTrasacao) as ValorTotal,
                (select crr.nome from carros crr where crr.id = c0.id) as NomeDoCarro,
                (select m1.nome from modelos m1 where m1.id = m.id) as ModeloDoCarro
            FROM `Pedidos` AS `p`
            INNER JOIN `Clientes` AS `c` ON `p`.`ClienteId` = `c`.`Id`
            INNER JOIN `PedidoCarros` AS `p0` ON `p`.`Id` = `p0`.`PedidoId`
            INNER JOIN `Carros` AS `c0` ON `p0`.`CarroId` = `c0`.`Id`
            INNER JOIN `Modelos` AS `m` ON `c0`.`ModeloId` = `m`.`Id`
            INNER JOIN `Marcas` AS `m0` ON `m`.`MarcaId` = `m0`.`Id`
            group by 
                `p`.`Id` ,
                `c`.`Nome`,
                `m0`.`Nome`,
                `p`.`DataLocacao`,
                `p`.`DataEntrega`,
                NomeDoCarro,
                ModeloDoCarro
            having  sum(p0.ValorTrasacao)  > 100000
                
            */
            
            ViewBag.pedidos = pedidos;
            return View();
            // return StatusCode(200, pedidos);
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,DataLocacao")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                var config = _context.Configuracoes.FirstOrDefault();
                var dias = config is not null ? config.DiasDeLocacao : 1;
                pedido.DataEntrega = pedido.DataLocacao.AddDays(dias);
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return Redirect("/PedidoCarros?pedidoId=" + pedido.Id);
            }
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,DataLocacao,DataEntrega")] Pedido pedido)
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
