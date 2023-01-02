using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class SearchProductsViewModel
    {
        public SearchProductsViewModel(IList<Product> products, string search)
        {
            Products = products;
            Search = search;
        }

        public IList<Product> Products { get; }
        public string Search { get; set; }
    }
}
