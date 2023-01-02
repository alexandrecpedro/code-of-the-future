using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Model.Redis
{
    public interface IUserRedisRepository
    {
        Task<UserCounterData> GetUserCounterDataAsync(string customerId);
        Task AddUserNotificationAsync(string customerId, UserNotification userNotification);
        Task UpdateUserBasketCountAsync(string customerId, int userBasketCount);
        Task MarkAllAsReadAsync(string customerId);
    }
}