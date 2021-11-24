using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClothingShop.BusinessLogic.Repositories.Interfaces
{
    public interface IShopRepository
    {
        Task<AllProductModels> GetAllProduct();

        PaginationModel<ProductViewModel> GetProductList(string name, string sort, int? pageNumber, int? pageSize);

        Task<ProductDetailModel> GetProductDetails(int? id);

        Task<ProductDetailModel> GetBlankProductDetailModel();

        Task CreateProduct(ProductDetailModel product);

        Task<ProductDetailModel> EditProduct(ProductDetailModel product);

        Task DeleteProduct(int id);
    }
}