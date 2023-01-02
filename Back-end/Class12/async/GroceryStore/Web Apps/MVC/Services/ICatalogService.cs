using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface ICatalogService : IService
    {
        Task<IList<Product>> GetProducts();
        Task<IList<Product>> SearchProducts(string search);
        Task<Product> GetProduct(string code);
    }
}
