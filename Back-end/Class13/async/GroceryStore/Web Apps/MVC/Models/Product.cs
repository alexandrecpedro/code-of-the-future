using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models
{
    public class Product : BaseModel
    {
        public Product()
        {

        }

        [Required]
        public Category Category
        {
            get
            {
                return new Category(CategoryName) { Id = CategoryId };
            }
        }
        [Required]
        [DataMember]
        public string Code { get; private set; }
        [Required]
        [DataMember]
        public string Name { get; private set; }
        [Required]
        [DataMember]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; private set; }
        public string ImageURL { get { return $"/images/catalog/large_{Code}.jpg"; } }
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public string CategoryName { get; set; }

        public Product(string code, string name, decimal price, int categoryId, string categoryName)
        {
            this.Code = code;
            this.Name = name;
            this.Price = price;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
        }
    }
}
