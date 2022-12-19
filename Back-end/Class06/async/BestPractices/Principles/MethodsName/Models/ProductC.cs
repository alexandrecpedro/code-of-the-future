using Example.Principles.MethodsName.Interfaces;

namespace Example.Principles.MethodsName.Models;

internal class ProductC: IProduct
{
    public decimal Price { get; set; } = default!;
    public string Name { get; private set; } = default!;

    public IProduct Search(string productName)
    {
        return new ProductC() { Price = 10.0M, Name = productName };
    }
}