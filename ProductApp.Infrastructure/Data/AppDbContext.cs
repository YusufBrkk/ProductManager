using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Entities;

namespace ProductApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        // İleride Product için de DbSet ekleyeceğiz
    }
}