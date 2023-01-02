using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Model
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<CustomerBasket> AddBasketAsync(string customerId, BasketItem item);
        Task<UpdateQuantityOutput> UpdateBasketAsync(string customerId, UpdateQuantityInput item);
        Task<bool> DeleteBasketAsync(string id);
    }
}