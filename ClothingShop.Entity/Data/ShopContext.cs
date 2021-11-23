using Microsoft.EntityFrameworkCore;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Sample> Sample { get; set; }
    }
}