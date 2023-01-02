using Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Model.Redis
{
    public class UserRedisRepository : IUserRedisRepository
    {
        private const int USER_DB_INDEX = 1;
        private readonly ILogger<UserRedisRepository> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public UserRedisRepository(ILogger<UserRedisRepository> logger, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase(USER_DB_INDEX);
        }

        private IServer GetServer()
        {
            var endpoints = _redis.GetEndPoints();
            return _redis.GetServer(endpoints.First());
        }

        public async Task<UserCounterData> GetUserCounterDataAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException();

            UserCounterData userCounterData;
            var data = await _database.StringGetAsync(customerId);
            if (data.IsNullOrEmpty)
            {
                List<UserNotification> userNotifications = new List<UserNotification>();
                userCounterData = new UserCounterData(userNotifications, 0);
                await UpdateUserCounterDataAsync(customerId, userCounterData);
                return userCounterData;
            }

            userCounterData = JsonConvert.DeserializeObject<UserCounterData>(data);
            userCounterData.Notifications = userCounterData.Notifications.OrderByDescending(n => n.DateCreated).ToList();
            return userCounterData;
        }

        public async Task AddUserNotificationAsync(string customerId, UserNotification userNotification)
        {
            var userCounterData = await GetUserCounterDataAsync(customerId);
            userCounterData.Notifications.Add(userNotification);
            await UpdateUserCounterDataAsync(customerId, userCounterData);
        }

        public async Task UpdateUserBasketCountAsync(string customerId, int userBasketCount)
        {
            var userCounterData = await GetUserCounterDataAsync(customerId);
            userCounterData.BasketCount = userBasketCount;
            await UpdateUserCounterDataAsync(customerId, userCounterData);
        }

        public async Task MarkAllAsReadAsync(string customerId)
        {
            UserCounterData userCounterData = await GetUserCounterDataAsync(customerId);
            foreach (var notification in
                userCounterData.Notifications.Where(n => !n.DateVisualized.HasValue))
            {
                notification.DateVisualized = DateTime.Now;
            }
            await UpdateUserCounterDataAsync(customerId, userCounterData);
        }

        private async Task UpdateUserCounterDataAsync(string customerId, UserCounterData userCounterData)
        {
            var json = JsonConvert.SerializeObject(userCounterData);
            await _database.StringSetAsync(customerId, json);
        }
    }
}
