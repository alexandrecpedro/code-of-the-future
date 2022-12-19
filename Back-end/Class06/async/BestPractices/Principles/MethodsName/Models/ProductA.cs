using Example.Principles.MethodsName.Interfaces;

namespace Example.Principles.MethodsName.Models;

internal class ProductA: IProduct
{
    public decimal Price { get; set; } = default!;
    public string Name { get; private set; } = default!;
    
    public ProductA()
    {}

    public IProduct Search(string productName)
    {
        return new ProductA() { Price = 18.67M, Name = productName };
    }
}