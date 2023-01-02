using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class UrlAPIs
    {
        public static class Basket
        {
            public static string UpdateItemBasket(string baseUri) => $"{baseUri}/basket/items";
        }
    }
}
