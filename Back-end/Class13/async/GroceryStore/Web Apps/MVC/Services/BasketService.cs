using Infrastructure;
using Models;
using Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MVC;

namespace Services
{
    public class BasketService : BaseHttpService, IBasketService
    {
        class BasketUris
        {
            public static string GetBasket => "api/basket";
            public static string AddItem => "api/basket/additem";
            public static string UpdateItem => "api/basket/updateitem";
            public static string Checkout => "api/basket/checkout";
        }

        private readonly HttpClient _apiClient;
        private readonly string _basketUrl;
        private readonly ILogger<BasketService> _logger;

        public BasketService(
            IConfiguration configuration
            , HttpClient httpClient
            , ISessionHelper sessionHelper
            , ILogger<BasketService> logger)
            : base(configuration, httpClient, sessionHelper)
        {
            _apiClient = httpClient;
            _logger = logger;
            _baseUri = _configuration["BasketUrl"];
        }

        public async Task<CustomerBasket> GetBasket(string userId)
        {
            return await GetAuthenticatedAsync<CustomerBasket>(BasketUris.GetBasket, userId);
        }

        public async Task<CustomerBasket> AddItem(string customerId, BasketItem input)
        {
            var uri = $"{BasketUris.AddItem}/{customerId}";
            return await PostAsync<CustomerBasket>(uri, input);
        }

        public async Task<UpdateQuantityOutput> UpdateItem(string customerId, UpdateQuantityInput input)
        {
            var uri = $"{BasketUris.UpdateItem}/{customerId}";
            return await PutAsync<UpdateQuantityOutput>(uri, input);
        }

        public async Task<CustomerBasket> UpdateQuantities(ApplicationUser applicationUser, Dictionary<string, int> quantidades)
        {
            var uri = UrlAPIs.Basket.UpdateItemBasket(_basketUrl);

            var atualizarBasket = new
            {
                CustomerId = applicationUser.Id,
                Atualizacao = quantidades.Select(kvp => new
                {
                    ItemBasketId = kvp.Key,
                    NovaQuantidade = kvp.Value
                }).ToArray()
            };

            var conteudoBasket = new StringContent(JsonConvert.SerializeObject(atualizarBasket), System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PutAsync(uri, conteudoBasket);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerBasket>(jsonResponse);
        }

        public Task UpdateBasket(CustomerBasket customerBasket)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Checkout(string customerId, RegistrationViewModel viewModel)
        {
            var uri = $"{BasketUris.Checkout}/{customerId}";
            return await PostAsync<bool>(uri, viewModel);
        }

        public override string Scope => "Basket.API";
    }
}
