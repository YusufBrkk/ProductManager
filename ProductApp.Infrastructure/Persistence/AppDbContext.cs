namespace ProductApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        // ... diğer DbSet tanımları ve DbContext konfigürasyonu ...
    }
}