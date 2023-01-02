using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using MVC.Model.Redis;
using Polly.CircuitBreaker;
using Services;
using System;
using System.Threading.Tasks;

namespace Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService catalogService;

        public CatalogController
            (ICatalogService catalogService,
            ILogger<CatalogController> logger,
            IUserRedisRepository repository)
            : base(logger, repository)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await CheckUserCounterData();
                var products = await catalogService.GetProducts();
                var resultado = new SearchProductsViewModel(products, "");
                return base.View(resultado);
            }
            catch (BrokenCircuitException e)
            {
                logger.LogError(e, e.Message);
                HandleBrokenCircuitException(catalogService);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                HandleException();
            }

            return View();
        }

        public async Task<IActionResult> SearchProducts(string pesquisa)
        {
            try
            {
                await CheckUserCounterData();
                var products = await catalogService.SearchProducts(pesquisa);
                var resultado = new SearchProductsViewModel(products, pesquisa);
                return View("Index", resultado);
            }
            catch (BrokenCircuitException e)
            {
                logger.LogError(e, e.Message);
                HandleBrokenCircuitException(catalogService);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                HandleException();
            }

            return View("Index");
        }

    }
}

