namespace Example.Principles.ClassesName;

public class ClassesName
{
    decimal BadNaming()
    {
        List<decimal> p = new List<decimal>() { 5.5m, 1.48m };
        decimal t = 0;

        foreach (var i in p)
        {
            t += i;
        }

        return t;
    }

    decimal CleanVersion()
    {
        List<decimal> prices = new List<decimal>() { 5.5m, 1.48m };
        decimal total = 0;

        foreach (var price in prices)
        {
            total += price;
        }

        return total;
    }

    /** BAD NAMED CLASSES **/
    class Utility { }
    class Common { }
    class MyFunctions { }
    class BrenoUtils { }
    class ProcessorInfo { }

    /** CLEAR NAMED CLASSES **/
    class User { }
    class Account { }
    class QueryBuilder { }
    class ProductRepository { }
}