﻿using ClothingShop.Entity.Entities;
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

        PaginationModel<DiscountModel> GetDiscountList(string code, int? pageNumber, int? pageSize);

        Task<DiscountModel> GetDiscountDetails(int? id);

        Task CreateDiscount(DiscountModel discount);

        Task<DiscountModel> EditDiscount(DiscountModel discount);

        Task DeleteDiscount(int id);

        Task CreateVoucher(int VoucherNumber, int DiscountId);

        Task DeleteVoucher(int id);

        Task DeleteAllVoucher(int DiscountId);

        RankModel GetRank(int RankId);

        List<RankModel> GetAllRanks();

        Task UpdateRank(string UserId);

        Task<Point> AddPoint(string UserId, int OrderId, int value);

        List<VoucherModel> GetVoucherListByUser(string UserId);

        Task AddToCart(int SkuId, int Quantity, string UserId);

        Task UpdateCart(int CartId);

        Task RemoveFromCart(int ItemId);

        Task EmptyCart(int CartId);

        Task<Cart> GetCart(int CartId);

        Task<int> GetCartId(string UserId);

        Task CreateOrder(int CartId, Address Address);

        Task AcceptOrder(int OrderId);

        Task CancelOrder(int OrderId);

        PaginationModel<Order> GetOrderList(int? orderId, string status, int? pageNumber, int? pageSize);

        Task<Order> GetOrder(int OrderId);

        PaginationModel<Order> GetOrderHistory(string UserId, int? pageNumber, int? pageSize);
    }
}