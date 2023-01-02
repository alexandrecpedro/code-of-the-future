using Models;
using Microsoft.EntityFrameworkCore;

namespace MVC
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(t => t.Id);

            modelBuilder.Entity<Order>().HasKey(t => t.Id);
            modelBuilder.Entity<Order>().HasMany(t => t.Items).WithOne(t => t.Order);
            modelBuilder.Entity<Order>().HasOne(t => t.Registration).WithOne(t => t.Order).IsRequired();

            modelBuilder.Entity<OrderItem>().HasKey(t => t.Id);
            modelBuilder.Entity<OrderItem>().HasOne(t => t.Order);
            modelBuilder.Entity<OrderItem>().HasOne(t => t.Product);

            modelBuilder.Entity<Registration>().HasKey(t => t.Id);
            modelBuilder.Entity<Registration>().HasOne(t => t.Order);
        }
    }
}
