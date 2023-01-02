using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Model
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string customerId)
        {
            CustomerId = customerId;
            Items = new List<BasketItem>();
        }

        public CustomerBasket(CustomerBasket customerBasket)
        {
            this.CustomerId = customerBasket.CustomerId;
            this.Items = customerBasket.Items;
        }

        public string CustomerId { get; set; }
        public List<BasketItem> Items { get; set; }
        public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}