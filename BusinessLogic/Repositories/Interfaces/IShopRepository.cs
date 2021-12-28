using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingShop.BusinessLogic.Repositories.Interfaces
{
    public interface IShopRepository
    {
        Task<AllProductModels> GetAllProduct();

        PaginationModel<ProductViewModel> GetProductList(string name, string sort, int? category, int? pageNumber, int? pageSize);

        Task<ProductDetailModel> GetProductDetails(int? id);

        Task<ProductDetailModel> GetBlankProductDetailModel();

        Task CreateProduct(ProductDetailModel product);

        Task<ProductDetailModel> EditProduct(ProductDetailModel product);

        Task DeleteProduct(int id);

        List<CategoryModel> GetAllCategories();

        PaginationModel<CategoryModel> GetCategoryList(int? pageNumber, int? pageSize);

        Task<CategoryModel> GetCategoryDetails(int? id);

        Task CreateCategory(CategoryModel category);

        Task<CategoryModel> EditCategory(CategoryModel category);

        Task DeleteCategory(int id);

        PaginationModel<DiscountModel> GetDiscountList(string name, int? pageNumber, int? pageSize);

        Task<DiscountModel> GetDiscountDetails(int? id);

        Task CreateDiscount(DiscountModel discount);

        Task<DiscountModel> EditDiscount(DiscountModel discount);

        Task DeleteDiscount(int id);
    }
}