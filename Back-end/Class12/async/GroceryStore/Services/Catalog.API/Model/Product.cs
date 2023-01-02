using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Catalog.API.Model
{
    public class Product : BaseModel
    {
        public Product()
        {

        }

        [Required]
        public Category Category { get; private set; }
        [Required]
        [DataMember]
        public string Code { get; private set; }
        [Required]
        [DataMember]
        public string Name { get; private set; }
        [Required]
        public decimal Price { get; private set; }

        public Product(string code, string name, decimal price, Category category)
        {
            this.Code = code;
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }
    }
}
