using Ordering.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ordering
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasKey(t => t.Id);
            modelBuilder.Entity<Order>().HasMany(t => t.Items).WithOne(t => t.Order);

            modelBuilder.Entity<OrderItem>().HasKey(t => t.Id);
            modelBuilder.Entity<OrderItem>().HasOne(t => t.Order);
        }
    }
}
