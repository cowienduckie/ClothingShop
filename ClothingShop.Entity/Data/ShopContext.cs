using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingShop.Entity.Data
{
    public class ShopContext : IdentityDbContext<Users, Roles, string>
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductEntry> ProductEntry { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Size> Size { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<ProductCategory>()
                .HasKey(o => new { o.CategoryId, o.ProductId });
            modelBuilder.Entity<ProductEntry>()
                .HasKey(o => new { o.ProductId, o.ColorId, o.SizeId });
        }
    }
}