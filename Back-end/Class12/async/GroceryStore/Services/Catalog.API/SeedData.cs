using Catalog.API.Data;
using Catalog.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API
{
    internal class SeedData
    {
        internal static async Task EnsureSeedData(IServiceProvider services)
        {
            using (var scope = services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await CreateTables(context);

                await SaveProducts(context);
            }
        }

        private static async Task CreateTables(ApplicationDbContext context)
        {
            await CreateTableCategory(context);
            await CreateTableProduct(context);
        }

        private static async Task CreateTableCategory(ApplicationDbContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Category' (
                            'Id'        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'Name'      TEXT NOT NULL
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task CreateTableProduct(ApplicationDbContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Product' (
                            'Id'        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                        'Code' 	    TEXT NOT NULL,
                            'Name'      TEXT NOT NULL,
	                        'Price'     NUMERIC NOT NULL,
                            'CategoryId' INTEGER NOT NULL,
                                FOREIGN KEY(CategoryId) REFERENCES Category(Id)
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task ExecuteSqlCommandAsync(ApplicationDbContext context, string createTableSql)
        {
            await context.Database.ExecuteSqlCommandAsync(createTableSql);
        }

        private static async Task SaveProducts(ApplicationDbContext context)
        {
            var productDbSet = context.Set<Product>();
            var categoryDbSet = context.Set<Category>();

            var products = await GetProducts();

            foreach (var product in products)
            {
                var categoryDB =
                categoryDbSet
                    .Where(c => c.Name == product.category)
                    .SingleOrDefault();

                if (categoryDB == null)
                {
                    categoryDB = new Category(product.category);
                    await categoryDbSet.AddAsync(categoryDB);
                    await context.SaveChangesAsync();
                }

                string code = product.number.ToString("000");
                if (!productDbSet.Where(p => p.Code == code).Any())
                {
                    await productDbSet.AddAsync(new Product(code, product.name, 52.90m, categoryDB));
                }
            }
            await context.SaveChangesAsync();
        }

        static async Task<List<ProductData>> GetProducts()
        {
            var json = await File.ReadAllTextAsync("products.json");
            return JsonConvert.DeserializeObject<List<ProductData>>(json);
        }
    }

    public class ProductData
    {
        public int number { get; set; }
        public string name { get; set; }
        public string category { get; set; }
    }
}