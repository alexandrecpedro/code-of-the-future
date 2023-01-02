using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Messages.Events
{
    /// <image src="$(ProjectDir)\ed.png"/>
    public class CheckoutEvent : IntegrationEvent
    {
        public CheckoutEvent()
        {

        }

        public CheckoutEvent(
              string userId, string userName, string email, string phone
            , string address, string additionalAddress, string district
            , string city, string state, string zipCode
            , Guid requestId
            , IList<CheckoutEventItem> items)
        {
            UserId = userId;
            UserName = userName;
            City = city;
            Email = email;
            Phone = phone;
            Address = address;
            AdditionalAddress = additionalAddress;
            District = district;
            State = state;
            ZipCode = zipCode;
            RequestId = requestId;
            Items = 
                items
                    .Select(i => 
                        new CheckoutEventItem(
                            i.Id, 
                            i.ProductId, 
                            i.ProductName, 
                            i.UnitPrice, 
                            i.Quantity)).ToList();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderId { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Guid RequestId { get; set; }
        public List<CheckoutEventItem> Items { get; } = new List<CheckoutEventItem>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class CheckoutEventItem
    {
        public CheckoutEventItem()
        {

        }

        public CheckoutEventItem(string id, string productId, string productNome, decimal precoUnitario, int quantidade)
        {
            Id = id;
            ProductId = productId;
            ProductName = productNome;
            UnitPrice = precoUnitario;
            Quantity = quantidade;
        }

        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string ImageURL { get; set; }
    }

}
