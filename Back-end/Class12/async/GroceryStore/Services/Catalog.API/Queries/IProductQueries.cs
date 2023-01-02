using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Queries
{
    public interface IProductQueries
    {
        Task<IEnumerable<Product>> GetProductsAsync(string search = null);
        Task<Product> GetProductAsync(string code);
    }
}