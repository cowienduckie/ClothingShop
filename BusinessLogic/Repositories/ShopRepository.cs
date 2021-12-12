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
                else if (sort == "price") products = products.OrderBy(p => p.Price);
                else if (sort == "-price") products = products.OrderByDescending(p => p.Price);
            }

            return products;
        }

        public async Task<ProductDetailModel> GetBlankProductDetailModel()
        {
            var model = new ProductDetailModel();
            var colors = await _db.Color.ToListAsync();
            var sizes = await _db.Size.ToListAsync();
            var categories = await _db.Category.ToListAsync();

            for (int i = 0; i < colors.Count; ++i)
            {
                for (int j = 0; j < sizes.Count; ++j)
                {
                    model.Items.Add(new ItemModel
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

            for (int i = 0; i <  categories.Count; ++i)
            {
                model.Categories.Add(new CategoryModel
                {
                    CategoryId = categories[i].CategoryId,
                    Name = categories[i].Name,
                    Description = categories[i].Description
                });
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
                                                                        }).ToList(),
                                        Categories = p.ProductCategories.Select(pc => new CategoryModel
                                        {
                                            CategoryId = pc.Category.CategoryId,
                                            Name = pc.Category.Name,
                                            Description = pc.Category.Description
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
                                            .Select(i => new ProductEntry
                                            {
                                                ColorId = i.ColorId,
                                                SizeId = i.SizeId,
                                                Quantity = i.Quantity
                                            })
                                            .ToList(),
                ProductCategories = model.Categories.Where(c => c.IsSelected)
                                                    .Select(c => new ProductCategory
                                                    {
                                                        CategoryId = c.CategoryId
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

                product.LastModified = DateTime.Now;

                _db.Product.Update(product);

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

        public List<CategoryModel> GetAllCategories()
        {
            return _db.Category.Select(c => new CategoryModel
                                {
                                    CategoryId = c.CategoryId,
                                    Name = c.Name
                                })
                                .ToList();
        }

        public PaginationModel<CategoryModel> GetCategoryList(int? pageNumber, int? pageSize)
        {
            var querryCategories = _db.Category.AsQueryable();
            var total = querryCategories.Count();
            var PageSize = pageSize ?? 20;
            var PageNumber = pageNumber ?? 1;

            var categories= querryCategories.Skip(PageSize * (PageNumber - 1)).Take(PageSize).Select(c => new CategoryModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
            }).ToList();

            return new PaginationModel<CategoryModel>()
            {
                ItemList = categories,
                Total = total,
                PageSize = PageSize,
                PageNumber = PageNumber
            };
        }

        public async Task<CategoryModel> GetCategoryDetails(int? id)
        {
            if (id == null) return null;

            return await _db.Category.Where(c => c.CategoryId == id)
                                    .Select(c => new CategoryModel
                                    {
                                        CategoryId = c.CategoryId,
                                        Name = c.Name,
                                        Description = c.Description
                                    }).FirstOrDefaultAsync();
        }

        public async Task CreateCategory(CategoryModel model)
        {
            var now = DateTime.Now;

            var category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                CreateTime = now,
                LastModified = now,
            };

            await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();
        }

        public async Task<CategoryModel> EditCategory(CategoryModel model)
        {
            try
            {
                var category= await _db.Category.Where(c => c.CategoryId == model.CategoryId)
                                               .Select(c => c)
                                               .FirstOrDefaultAsync();
                if (category == null) return null;

                //Available fields for editing
                category.Name = model.Name;
                category.Description = model.Description;

                category.LastModified = DateTime.Now;

                _db.Category.Update(category);

                await _db.SaveChangesAsync();

                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HasCategoryId(model.CategoryId))
                {
                    Console.WriteLine("Error DBupdate");
                }
                return null;
            }
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _db.Category.Where(c => c.CategoryId == id)
                                               .Select(c => c)
                                               .FirstOrDefaultAsync();

            if (category != null)
            {
                _db.Category.Remove(category);

                await _db.SaveChangesAsync();
            }
        }

        private bool HasCategoryId(int id)
        {
            return _db.Category.Any(c => c.CategoryId == id);
        }
    }
}