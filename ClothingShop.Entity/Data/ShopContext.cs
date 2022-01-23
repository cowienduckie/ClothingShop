using ClothingShop.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingShop.Entity.Data
{
    public class ShopContext : IdentityDbContext<Users, Roles, string, IdentityUserClaim<string>,
        UserRoles, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
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
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<ProductDiscount> ProductDiscount { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<Rank> Rank { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet")) entityType.SetTableName(tableName.Substring(6));
            }

            //Notification
            modelBuilder.Entity<Notification>()
                .HasKey(n => new {n.NotificationId});
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);

            //Product_Category
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new {pc.CategoryId, pc.ProductId});

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            //Product_Entry
            modelBuilder.Entity<ProductEntry>()
                .HasKey(pe => new {pe.SkuId});

            modelBuilder.Entity<ProductEntry>()
                .HasOne(pe => pe.Product)
                .WithMany(p => p.ProductEntries)
                .HasForeignKey(pe => pe.ProductId);

            modelBuilder.Entity<ProductEntry>()
                .HasOne(pe => pe.Color)
                .WithMany(c => c.ProductEntries)
                .HasForeignKey(pe => pe.ColorId);

            modelBuilder.Entity<ProductEntry>()
                .HasOne(pe => pe.Size)
                .WithMany(s => s.ProductEntries)
                .HasForeignKey(pe => pe.SizeId);

            //CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.SKU)
                .WithMany(pe => pe.CartItems)
                .HasForeignKey(ci => ci.SkuId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            //Cart
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Users>(u => u.CartId);

            //OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.SKU)
                .WithMany(pe => pe.OrderItems)
                .HasForeignKey(oi => oi.SkuId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            //Product_Discount
            modelBuilder.Entity<ProductDiscount>()
                .HasKey(pd => new {pd.ProductId, pd.DiscountId});

            modelBuilder.Entity<ProductDiscount>()
                .HasOne(pd => pd.Product)
                .WithMany(p => p.ProductDiscounts)
                .HasForeignKey(pd => pd.ProductId);

            modelBuilder.Entity<ProductDiscount>()
                .HasOne(pd => pd.Discount)
                .WithMany(d => d.ProductDiscounts)
                .HasForeignKey(pd => pd.DiscountId);

            //Voucher
            modelBuilder.Entity<Voucher>()
                .HasOne(v => v.Discount)
                .WithMany(d => d.Vouchers)
                .HasForeignKey(v => v.DiscountId);

            modelBuilder.Entity<Voucher>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vouchers)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //Rank
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Rank)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RankId);

            modelBuilder.Entity<Rank>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Rank)
                .HasForeignKey(u => u.RankId)
                .OnDelete(DeleteBehavior.Restrict);

            //Points
            modelBuilder.Entity<Point>()
                .HasOne(p => p.User)
                .WithMany(u => u.Points)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.Points)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Point>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Point)
                .HasForeignKey<Order>(o => o.PointId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Point)
                .WithOne(p => p.Order)
                .HasForeignKey<Point>(p => p.OrderId);

            //Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            //Address
            modelBuilder.Entity<Address>()
                .HasMany(a => a.Orders)
                .WithOne(o => o.Address)
                .HasForeignKey(o => o.AddressId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AddressId);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            //UserRole
            modelBuilder.Entity<UserRoles>(userRole =>
            {
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
            });
        }
    }
}