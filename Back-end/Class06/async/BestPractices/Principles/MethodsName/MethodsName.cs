using Example.Principles.MethodsName.Interfaces;
using Example.Principles.MethodsName.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace Example.Principles.MethodsName;

public class MethodsName
{
    /** DUPLICATION EXAMPLE **/
    string AddResKeysToUrl(Dictionary<string, string> paramDict, string url)
    {
        // url = "https://example.net/resource";

        string newUrl = url;

        newUrl += newUrl + "?";
        if (paramDict.ContainsKey("ResKey"))
        {
            newUrl += "resKey=" + paramDict["ResKey"];
        }

        if (paramDict.ContainsKey("ResSize"))
        {
            if (newUrl.Contains("resKey"))
            {
                newUrl += newUrl + "&";
            }
            newUrl += "resSize=" + paramDict["ResSize"];
        }

        if (paramDict.ContainsKey("ResQtt"))
        {
            if (newUrl.Contains("resKey") || newUrl.Contains("resSize"))
            {
                newUrl += newUrl + "&";
            }
            newUrl += "resQtt=" + paramDict["ResQtt"];
        }

        return newUrl;
    }

    /** CLEAR METHOD NAME**/
    string TransformDictionaryIntoQueryString(Dictionary<string, string> paramDict, string url)
    {
        var newUrl = new Uri(QueryHelpers.AddQueryString(url, paramDict));

        return newUrl.ToString();
    }

    /** BAD CODE**/

    decimal SearchProductCost(string productName, string productType)
    {
        decimal result = 0;
        if (productType == "productA")
        {
            ProductA resultA = (ProductA) new ProductA().Search(productName);
            result = resultA.Price;
        }
        if (productType == "productB")
        {
            ProductB resultB = (ProductB) new ProductB().Search(productName);
            result = resultB.Price;
        }
        if (productType == "productC")
        {
            ProductC resultC = (ProductC) new ProductC().Search(productName);
            result = resultC.Price;
        }

        return result;
    }

    /** CLEAR CODE **/
    decimal GetProductCost(string productName, string productType)
    {
        IProduct product = ProductFactory.GetProduct(productType);

        return product.Search(productName).Price;
    }

    

    /** BAD NAMED METHODS **/
    // public void Go() { }
    // public void Complete() { }
    // public void Get() { }
    // public void Process() { }
    // public void Dolt() { }
    // public void Start() { }
    // public void On_Init() { }
    // public void Page_Load() { }

    /** GOOD NAMED METHODS **/
    // public void GetRegisteredUsers() { }
    // public void IsValidSubmission() { }
    // public void ImportDocument() { }
    // public void SendEmail() { }
}