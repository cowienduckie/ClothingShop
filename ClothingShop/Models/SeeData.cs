using ClothingShop.Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ShopContext>>()))
            {
                // Look for any movies.
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }

                context.Product.AddRange(
                    new ClothingShop.Entity.Models.Product
                    {
                        Name_Product = "When Harry Met Sally",
                        DateCreated_Product = DateTime.Parse("1989-2-12"),
                        Type_Product = "Romantic Comedy",
                        Price_Product = 7.99M
                    },

                    new ClothingShop.Entity.Models.Product
                    {
                        Name_Product = "When Harry Met Sally",
                        DateCreated_Product = DateTime.Parse("1989-2-12"),
                        Type_Product = "Romantic Comedy",
                        Price_Product = 7.99M
                    },

                    new ClothingShop.Entity.Models.Product
                    {
                        Name_Product = "When Harry Met Sally",
                        DateCreated_Product = DateTime.Parse("1989-2-12"),
                        Type_Product = "Romantic Comedy",
                        Price_Product = 7.99M
                    },

                    new ClothingShop.Entity.Models.Product
                    {
                        Name_Product = "When Harry Met Sally",
                        DateCreated_Product = DateTime.Parse("1989-2-12"),
                        Type_Product = "Romantic Comedy",
                        Price_Product = 7.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}