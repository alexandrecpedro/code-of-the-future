namespace Catalog.API.Queries
{
    public class Product
    {
        public Product()
        {

        }

        public Product(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
