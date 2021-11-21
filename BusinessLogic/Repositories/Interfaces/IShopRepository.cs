using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClothingShop.BusinessLogic.Repositories.Interfaces
{
    public interface IShopRepository
    {
        public Task<AllProductModels> GetAllProduct();

        public Task<ProductDetailModels> GetDetails(int? id);

        public Task CreateProduct(ProductDetailModels product);

        public Task EditProduct(ProductDetailModels product);

        public Task DeleteProduct(int id);
    }
}