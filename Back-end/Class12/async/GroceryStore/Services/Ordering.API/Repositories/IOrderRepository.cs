using Ordering.Models;
using Ordering.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrUpdate(Order order);
        Task<IList<Order>> GetOrders(string customerId);
    }
}
