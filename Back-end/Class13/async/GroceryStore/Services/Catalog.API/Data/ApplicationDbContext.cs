using Catalog.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var category = modelBuilder.Entity<Category>();
            category.HasKey(t => t.Id);
            category.Property("Name").HasColumnType("nvarchar(255)");

            var product = modelBuilder.Entity<Product>();
            product.HasKey(t => t.Id);
            product.Property("Code").HasColumnType("nvarchar(3)");
            product.Property("Name").HasColumnType("nvarchar(255)");
            product.Property("Price").HasColumnType("decimal(5,2)");
        }
    }
}




