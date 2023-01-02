using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Model
{
    public class BasketItem : IValidatableObject
    {
        public BasketItem()
        {

        }

        public BasketItem(string id, string productId, string productName, decimal unitPrice, int quantity)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public string Id { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;
        public string ImageURL { get { return $"/images/catalog/large_{ProductId}.jpg"; } }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Quantity < 1)
            {
                results.Add(new ValidationResult("Invalid quantity", new[] { "Quantity" }));
            }

            return results;
        }
    }
}