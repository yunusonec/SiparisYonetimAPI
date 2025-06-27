using Microsoft.EntityFrameworkCore;
using SiparisYonetimAPI.Models;

namespace SiparisYonetimAPI.Data
{
    public class SiparisContext : DbContext
    {
        public SiparisContext(DbContextOptions<SiparisContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Örnek verilerle sistemi test etmek için 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Kitap A", Price = 75.50m, Stock = 50 },
                new Product { Id = 2, Name = "Kalem B", Price = 12.00m, Stock = 100 },
                new Product { Id = 3, Name = "Defter C", Price = 30.25m, Stock = 10 }
            );
        }
    }
}
