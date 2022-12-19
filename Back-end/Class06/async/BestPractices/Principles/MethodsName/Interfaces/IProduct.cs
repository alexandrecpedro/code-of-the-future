namespace Example.Principles.MethodsName.Interfaces;

internal interface IProduct
{
    decimal Price { get; set; }

    IProduct Search(string productName);
}