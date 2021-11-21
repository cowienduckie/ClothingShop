using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClothingShop.BusinessLogic.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopContext _db;

        public ShopRepository(ShopContext db)
        {
            _db = db;
        }



        public async Task<AllProductModels> GetAllProduct()
        {
            var product = await _db.Product.ToListAsync();

            var allProductModels = new AllProductModels {
                ProductList = new List<ProductViewModels>()
            };

            product.ForEach(m =>
            {
                var productViewModels = new ProductViewModels
                {
                    ProductId = m.ProductId,
                    Name = m.Name,
                    Image = m.Image,
                    Price = m.Price
                };

                allProductModels.ProductList.Add(productViewModels);
            });

            return allProductModels;
        }

        public async Task<ProductDetailModels> GetDetails(int? id)
        {
            if (id == null) return null;

            var product = await _db.Product.FirstOrDefaultAsync(s => s.ProductId == id);

            if (product == null) return null;

            var productDetailModels = new ProductDetailModels()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Image = product.Image,
                Price = product.Price,
                Discount = product.Discount,
                Discription = product.Discription,
                CreateTime = product.CreateTime,
                LastModified = product.LastModified
            };

            return productDetailModels;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _db.Product.FirstOrDefaultAsync(s => s.ProductId == id);

            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
        }

        private bool ProductExists(int id)
        {
            return _db.Product.Any(e => e.ProductId == id);
        }

        public async Task EditProduct(ProductDetailModels product)
        {
            try
            {
                _db.Update(product);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    Console.WriteLine("Error DBupdate");
                }
            }
        }

        public async Task CreateProduct(ProductDetailModels product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
        }

    }
}