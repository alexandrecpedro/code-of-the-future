using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Order : BaseModel
    {
        public Order()
        {
            Registration = new Registration();
        }

        public Order(Registration registration)
        {
            Registration = registration;
        }

        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        [Required]
        public virtual Registration Registration { get; private set; }
    }
}
