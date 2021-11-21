using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClothingShop.BusinessLogic.Repositories.Interfaces
{
    public interface IShopRepository
    {
        public Task<AllProductModels> GetAllProduct();

        public Task<ProductDetailModels> GetDetails(int? id);

        public Task DeleteProduct(int id);

    }
}