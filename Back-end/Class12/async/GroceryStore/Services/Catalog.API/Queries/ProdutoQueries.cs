using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IConfiguration configuration;

        public ProductQueries(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string search = null)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var sql =
                    "select p.Id, p.Code, p.Name, p.Price," +
                    "   c.Id as CategoryId, c.Name as CategoryName" +
                    " from product as p " +
                    " inner join category as c " +
                    "   on c.Id = p.CategoryId";
                if (string.IsNullOrWhiteSpace(search))
                {
                    return await connection.QueryAsync<Product>(sql);
                }
                sql += " where p.name like @search or c.name like @search";
                return await connection.QueryAsync<Product>(sql, new { search = "%" + search + "%" });
            }
        }

        public async Task<Product> GetProductAsync(string code)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var sql = "select Id, Code, Name, Price from product where Code = @code";
                return (await connection.QueryAsync<Product>(sql, new { code })).SingleOrDefault();
            }
        }
    }
}
