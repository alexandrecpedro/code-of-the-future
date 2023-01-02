using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {

        }

        public CustomerBasket(string customerId, List<BasketItem> items)
        {
            CustomerId = customerId;
            Items = items;
        }

        public string CustomerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
    }

    public class BasketItem
    {
        public BasketItem()
        {

        }

        public BasketItem(string id, string productId, string productName, decimal unitPrice, int quantity, string imageURL)
        {   
            Id = id;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            ImageURL = imageURL;
        }

        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;
        public string ImageURL { get; set; }
    }
}
