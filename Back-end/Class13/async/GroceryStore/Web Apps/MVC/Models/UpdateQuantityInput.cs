using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MVC.Models
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
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
