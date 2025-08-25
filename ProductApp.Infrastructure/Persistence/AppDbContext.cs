using Microsoft.EntityFrameworkCore;
using ProductApp.Domain.Entities; // Product entity'nin bulunduğu namespace

namespace ProductApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        // ... diğer DbSet tanımları ve DbContext konfigürasyonu ...
    }
}