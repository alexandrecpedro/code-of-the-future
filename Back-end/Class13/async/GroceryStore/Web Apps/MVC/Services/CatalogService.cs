using Models;
using Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MVC;

namespace Services
{
    public class CatalogService : BaseHttpService, ICatalogService
    {
        class ApiUris
        {
            public static string GetProduct => "api/product";
            public static string SearchProducts => "api/search";
        }

        private readonly ILogger<CatalogService> _logger;

        public CatalogService(
            IConfiguration configuration
            , HttpClient httpClient
            , ISessionHelper sessionHelper
            , ILogger<CatalogService> logger)
            : base(configuration, httpClient, sessionHelper)
        {
            _logger = logger;
            _baseUri = _configuration["CatalogUrl"];
        }

        public async Task<IList<Models.Product>> GetProducts()
        {
            var uri = _baseUri + ApiUris.GetProduct;
            var json = await _httpClient.GetStringAsync(uri);
            if (json == null)
            {
                return new List<Product>();
            }

            IList<Product> result = JsonConvert.DeserializeObject<IList<Models.Product>>(json);
            return result;
        }

        public async Task<IList<Product>> SearchProducts(string search)
        {
            return await GetAsync<List<Product>>(ApiUris.SearchProducts, search);
        }

        public async Task<Models.Product> GetProduct(string code)
        {
            return await GetAsync<Product>(ApiUris.GetProduct, code);
        }

        public override string Scope => "Catalog.API";
    }
}
