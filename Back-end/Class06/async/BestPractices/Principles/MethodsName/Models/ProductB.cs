using Example.Principles.MethodsName.Interfaces;

namespace Example.Principles.MethodsName.Models;

internal class ProductB: IProduct
{
    public decimal Price { get; set; } = default!;
    public string Name { get; private set; } = default!;
    
    public ProductB()
    {}

    public IProduct Search(string productName)
    {
        return new ProductB() { Price = 12.0M, Name = productName };
    }
}