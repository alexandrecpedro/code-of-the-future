using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class OrderConfirmed
    {
        public OrderConfirmed()
        {

        }

        public OrderConfirmed(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
