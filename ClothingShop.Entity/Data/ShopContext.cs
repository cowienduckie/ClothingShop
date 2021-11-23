using ClothingShop.Entity.Entities;
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
                .HasKey(pc => new { pc.CategoryId, pc.ProductId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne<Product>(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne<Category>(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<ProductEntry>()
                .HasKey(pe => new { pe.ProductId, pe.ColorId, pe.SizeId });

            modelBuilder.Entity<ProductEntry>()
                .HasOne<Product>(pe => pe.Product)
                .WithMany(p => p.ProductEntries)
                .HasForeignKey(pe => pe.ProductId);

            modelBuilder.Entity<ProductEntry>()
                .HasOne<Color>(pe => pe.Color)
                .WithMany(c => c.ProductEntries)
                .HasForeignKey(pe => pe.ColorId);

            modelBuilder.Entity<ProductEntry>()
                .HasOne<Size>(pe => pe.Size)
                .WithMany(s => s.ProductEntries)
                .HasForeignKey(pe => pe.SizeId);
        }
    }
}