using Example.Principles.MethodsName.Interfaces;
using Example.Principles.MethodsName.Models;

namespace Example.Principles.MethodsName;

internal class ProductFactory
{
    internal static IProduct GetProduct(string productType)
    {
        switch (productType)
        {
            case "productA":
                return new ProductA();
            case "productB":
                return new ProductB();
            default:
                return new ProductC();
        }
    }
}