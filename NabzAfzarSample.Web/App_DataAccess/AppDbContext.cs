using System.Data.Entity;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.App_DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=NabzAfzarSampleDb")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}