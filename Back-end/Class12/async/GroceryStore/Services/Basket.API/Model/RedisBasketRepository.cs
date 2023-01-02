using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Model
{
    public class RedisBasketRepository : IBasketRepository
    {
        private const int BASKET_DB_INDEX = 0;
        private readonly ILogger<RedisBasketRepository> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ILogger<RedisBasketRepository> logger, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase(BASKET_DB_INDEX);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException();

            var data = await _database.StringGetAsync(customerId);
            if (data.IsNullOrEmpty)
            {
                return await UpdateBasketAsync(new CustomerBasket(customerId));
            }
            return JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            return server.Keys()?.Select(k => k.ToString());
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var criado = await _database.StringSetAsync(basket.CustomerId, JsonConvert.SerializeObject(basket));
            if (!criado)
            {
                _logger.LogError("Error while updating customer basket.");
                return null;
            }
            return await GetBasketAsync(basket.CustomerId);
        }

        public async Task<CustomerBasket> AddBasketAsync(string customerId, BasketItem item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(item.ProductId))
                throw new ArgumentException();

            if (item.Quantity <= 0)
                throw new ArgumentOutOfRangeException();

            var basket = await GetBasketAsync(customerId);
            BasketItem itemDB = basket.Items.Where(i => i.ProductId == item.ProductId).SingleOrDefault();
            if (itemDB == null)
            {
                itemDB = new BasketItem(item.Id, item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);
                basket.Items.Add(item);
            }
            return await UpdateBasketAsync(basket);
        }

        public async Task<UpdateQuantityOutput> UpdateBasketAsync(string customerId, UpdateQuantityInput item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(item.ProductId))
                throw new ArgumentException();

            if (item.Quantity < 0)
                throw new ArgumentOutOfRangeException();

            var basket = await GetBasketAsync(customerId);
            BasketItem itemDB = basket.Items.Where(i => i.ProductId == item.ProductId).SingleOrDefault();
            itemDB.Quantity = item.Quantity;
            if (item.Quantity == 0)
            {
                basket.Items.Remove(itemDB);
            }
            CustomerBasket customerBasket = await UpdateBasketAsync(basket);
            return new UpdateQuantityOutput(itemDB, customerBasket);
        }

        private IServer GetServer()
        {
            var endpoints = _redis.GetEndPoints();
            return _redis.GetServer(endpoints.First());
        }

    }
}
