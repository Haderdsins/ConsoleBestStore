using ConsoleBestStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleBestStore.DAL.DataBase;

public class ShopDbContext : DbContext
{
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OmgItem> OmgItems { get; set; }

    public ShopDbContext()
    {
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=store_db;Username=postgres;Password=postgres");
        }
    }

}