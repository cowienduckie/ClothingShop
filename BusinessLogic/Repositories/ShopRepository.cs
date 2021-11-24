using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ClothingShop.Entity.Entities;

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
            var product = await _db.Product.Select(m => new ProductViewModel
            {
                ProductId = m.ProductId,
                Name = m.Name,
                Image = m.Image,
                Price = m.Price
            }).ToListAsync();

            return new AllProductModels
            {
                ProductList = product
            };
        }

        public PaginationModel<ProductViewModel> GetProductList(string name, string sort, int? pageNumber, int? pageSize)
        {
            var queryProducts = IQueryProductList(name, sort);
            var total = queryProducts.Count();
            var PageSize = pageSize ?? 20;
            var PageNumber = pageNumber ?? 1;

            var products = queryProducts.Skip(PageSize * (PageNumber - 1)).Take(PageSize).Select(p => new ProductViewModel()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Image = p.Image,
                Price = p.Price,
                Stock = p.ProductEntries.Sum(pe => pe.Quantity)
            }).ToList();

            return new PaginationModel<ProductViewModel>()
            {
                ItemList = products,
                Total = total,
                PageSize = PageSize,
                PageNumber = PageNumber
            };
        }

        private IQueryable<Product> IQueryProductList(string name, string sort)
        {
            var products = _db.Product.AsQueryable();

            //Search filters
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            //Sort filters
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "name") products = products.OrderBy(p => p.Name);
                else if (sort == "-name") products = products.OrderByDescending(p => p.Name);
            }

            return products;
        }

        public async Task<ProductDetailModel> GetBlankProductDetailModel()
        {
            var model = new ProductDetailModel();
            var colors = await _db.Color.ToListAsync();
            var sizes = await _db.Size.ToListAsync();

            for (int i = 0; i < colors.Count; ++i)
            {
                for (int j = 0; j < sizes.Count; ++j)
                {
                    model.Items.Add(new ItemModel()
                    {
                        ColorId = colors[i].ColorId,
                        ColorValue = colors[i].Value,
                        ColorHexCode = colors[i].ColorHexCode,
                        SizeId = sizes[j].SizeId,
                        SizeValue = sizes[j].Value,
                        Quantity = 0
                    });
                }
            }

            return model;
        }

        public async Task<ProductDetailModel> GetProductDetails(int? id)
        {
            if (id == null) return null;

            return await _db.Product.Where(p => p.ProductId == id)
                                    .Select(p => new ProductDetailModel()
                                    {
                                        ProductId = p.ProductId,
                                        Name = p.Name,
                                        Image = p.Image,
                                        Price = p.Price,
                                        Discount = p.Discount,
                                        Description = p.Description,
                                        Stock = p.ProductEntries.Sum(pe => pe.Quantity),
                                        CreateTime = p.CreateTime,
                                        LastModified = p.LastModified,
                                        Items = p.ProductEntries.Select(pe => new ItemModel()
                                                                        {
                                                                            ColorId = pe.ColorId,
                                                                            ColorValue = pe.Color.Value,
                                                                            ColorHexCode = pe.Color.ColorHexCode,
                                                                            SizeId = pe.SizeId,
                                                                            SizeValue = pe.Size.Value,
                                                                            SKU = pe.SKU,
                                                                            Quantity = pe.Quantity
                                                                        }).ToList()
                                    }).FirstOrDefaultAsync();
        }

        public async Task CreateProduct(ProductDetailModel model)
        {
            var now = DateTime.Now;
            const string defaultImage = "https://imgur.com/a/tZDUgE8";

            var product = new Product
            {
                Name = model.Name,
                Image = string.IsNullOrEmpty(model.Image) ? defaultImage : model.Image,
                Price = model.Price,
                Discount = model.Discount,
                Description = model.Description,
                CreateTime = now,
                LastModified = now,
                ProductEntries = model.Items.Where(i => i.Quantity != 0)
                                            .Select(i => new ProductEntry()
                                            {
                                                ColorId = i.ColorId,
                                                SizeId = i.SizeId,
                                                Quantity = i.Quantity
                                            })
                                            .ToList()
            };
            
            await _db.Product.AddAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task<ProductDetailModel> EditProduct(ProductDetailModel model)
        {
            try
            {
                var product = await _db.Product.Where(p => p.ProductId == model.ProductId)
                                               .Select(p => p)
                                               .FirstOrDefaultAsync();
                if (product == null) return null;

                //Available fields for editing
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.Discount = model.Discount;

                await _db.SaveChangesAsync();

                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HasProductId(model.ProductId))
                {
                    Console.WriteLine("Error DBupdate");
                }
                return null;
            }
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _db.Product.Where(p => p.ProductId == id)
                                           .Select(p => p)
                                           .FirstOrDefaultAsync();

            if (product != null)
            {
                _db.Product.Remove(product);

                await _db.SaveChangesAsync();
            }
        }

        private bool HasProductId(int id)
        {
            return _db.Product.Any(p => p.ProductId == id);
        }
    }
}