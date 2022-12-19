namespace Example.Principles.CodeIndentation;

public class CodeIndentationEx2
{
    // REFACTORING THE CODE - WRONG PART
    void RegisterProduct(string productName, string productType)
    {
        if (!string.IsNullOrEmpty(productName))
        {
            if (!string.IsNullOrEmpty(productType))
            {
                // register a product
            }
            else
            {
                throw new ArgumentException("ProductType is mandatory!");
            }
        }
        else
        {
            throw new ArgumentException("ProductName is mandatory!");
        }
    }

    // REFACTORING THE CODE - CORRECT PART
    void PostProduct(string productName, string productType)
    {
        if (string.IsNullOrEmpty(productName))
        {
            throw new ArgumentException("ProductName is mandatory!");
        }
        
        if (string.IsNullOrEmpty(productType))
        {
            throw new ArgumentException("ProductType is mandatory!");
        }

        // register a product
    }
}