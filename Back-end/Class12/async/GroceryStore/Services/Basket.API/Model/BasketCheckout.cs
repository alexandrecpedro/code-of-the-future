using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Model
{
    public class BasketCheckout
    {
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Customer { get; set; }
        public Guid RequestId { get; set; }
    }
}
