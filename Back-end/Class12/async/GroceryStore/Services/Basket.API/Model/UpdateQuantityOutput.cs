using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Model
{
    public class UpdateQuantityOutput
    {
        public UpdateQuantityOutput(BasketItem basketItem, CustomerBasket customerBasket)
        {
            BasketItem = basketItem;
            CustomerBasket = customerBasket;
        }

        public BasketItem BasketItem { get; }
        public CustomerBasket CustomerBasket { get; }
    }
}
