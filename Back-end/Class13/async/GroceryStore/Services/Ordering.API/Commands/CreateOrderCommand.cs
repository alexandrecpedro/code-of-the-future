using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ordering.Commands
{
    public class CreateOrderCommand
        : IRequest<bool>
    {
        public CreateOrderCommand()
        {

        }

        public CreateOrderCommand(Guid idempotencyId, List<CreateOrderCommandItem> items, string customerId, string customerName, string customerEmail, string customerPhone, string customerAddress, string customerAdditionalCustomer, string customerDistrict, string customerCity, string customerState, string customerZipCode)
        {
            IdempotencyId = idempotencyId;
            Items = items;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            CustomerAddress = customerAddress;
            CustomerAdditionalCustomer = customerAdditionalCustomer;
            CustomerDistrict = customerDistrict;
            CustomerCity = customerCity;
            CustomerState = customerState;
            CustomerZipCode = customerZipCode;
        }

        public Guid IdempotencyId { get; set; } = Guid.Empty;
        public List<CreateOrderCommandItem> Items { get; private set; } = new List<CreateOrderCommandItem>();
        public string CustomerId { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public string CustomerPhone { get; set; } = "";
        public string CustomerAddress { get; set; } = "";
        public string CustomerAdditionalCustomer { get; set; } = "";
        public string CustomerDistrict { get; set; } = "";
        public string CustomerCity { get; set; } = "";
        public string CustomerState { get; set; } = "";
        public string CustomerZipCode { get; set; } = "";
    }

    public class CreateOrderCommandItem
    {
        public CreateOrderCommand Order { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public decimal Subtotal => ProductQuantity * ProductUnitPrice;

        public CreateOrderCommandItem()
        {

        }

        public CreateOrderCommandItem(string productCodigo, string productNome, int productQuantity, decimal productUnitPrice)
        {
            ProductCode = productCodigo;
            ProductName = productNome;
            ProductQuantity = productQuantity;
            ProductUnitPrice = productUnitPrice;
        }

        public void AtualizaQuantidade(int productQuantity)
        {
            ProductQuantity = productQuantity;
        }
    }
}
