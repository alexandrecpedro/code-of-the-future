using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Model
{
    public class UpdateQuantityInput
    {
        public UpdateQuantityInput()
        {

        }

        public UpdateQuantityInput(string productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        [Required]
        public string ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
