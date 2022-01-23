using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IShopRepository _shopRepository;
        private readonly UserManager<Users> _userManager;

        public ProductController(IShopRepository shopRepository,
            UserManager<Users> userManager,
            INotyfService notyf)
        {
            _shopRepository = shopRepository;
            _userManager = userManager;
            _notyf = notyf;
        }

        //GET: Product
        [HttpGet]
        [Route("Product")]
        public IActionResult Index(string name, string sort, int? category, int? pageNumber, int? pageSize)
        {
            try
            {
                pageSize ??= 9; //9 items per page
                var model = _shopRepository.GetProductList(name, sort, category, pageNumber, pageSize);

                //View bag
                if (name != null) ViewBag.Name = name;
                if (sort != null) ViewBag.Sort = sort;
                if (category != null) ViewBag.Category = category;
                ViewBag.Categories = _shopRepository.GetAllCategories();

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy danh sách sản phẩm");
                return RedirectToAction("Index", "Home");
            }
        }

        //GET: Product/Details/id
        [HttpGet]
        [Route("Product/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã sản phẩm");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var model = await _shopRepository.GetProductDetails(id);

                if (model == null)
                {
                    _notyf.Error("Không tìm thấy sản phẩm");
                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy chi tiết sản phẩm");
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/AddToCart
        [Authorize]
        [Route("Product/AddToCart")]
        public async Task<IActionResult> AddToCart(int? SkuId, int Quantity = 1)
        {
            if (SkuId == null) return RedirectToAction("Index", "Product");

            try
            {
                var user = await GetLoggedUser();

                await _shopRepository.AddToCart(SkuId.Value, Quantity, user.Id);
                _notyf.Success("Thêm sản phẩm thành công");
                return RedirectToAction("UpdateCart", "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi thêm sản phẩm");
                return RedirectToAction("Index", "Product");
            }
        }

        //DELETE: Product/RemoveFromCart
        [Authorize]
        [Route("Product/RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(int? ItemId)
        {
            if (ItemId == null)
            {
                _notyf.Error("Không có mã sản phẩm");
                return RedirectToAction("Index", "Product");
            }

            try
            {
                await _shopRepository.RemoveFromCart(ItemId.Value);
                _notyf.Success("Xóa sản phẩm thành công");
                return RedirectToAction("UpdateCart", "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi xóa sản phẩm");
                return RedirectToAction("Index", "Product");
            }
        }

        //PUT: Product/UpdateCart
        [Authorize]
        [Route("Product/UpdateCart")]
        public async Task<IActionResult> UpdateCart()
        {
            try
            {
                var user = await GetLoggedUser();
                await _shopRepository.UpdateCart(await _shopRepository.GetCartId(user.Id));
                _notyf.Success("Cập nhật giỏ hàng thành công");
                return RedirectToAction("ShowCart", "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi cập nhật giỏ hàng");
                return RedirectToAction("Index", "Product");
            }
        }

        //DELETE: Product/EmptyCart
        [Authorize]
        [Route("Product/EmptyCart")]
        public async Task<IActionResult> EmptyCart()
        {
            try
            {
                var user = await GetLoggedUser();
                await _shopRepository.EmptyCart(await _shopRepository.GetCartId(user.Id));

                return RedirectToAction("UpdateCart", "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi làm trống giỏ hàng");
                return RedirectToAction("Index", "Product");
            }
        }

        //GET: Product/ShowCart
        [Authorize]
        [HttpGet]
        [Route("Product/ShowCart")]
        public async Task<IActionResult> ShowCart()
        {
            try
            {
                var user = await GetLoggedUser();
                var model = await _shopRepository.GetCart(await _shopRepository.GetCartId(user.Id));

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy giỏ hàng");
                return RedirectToAction("Index", "Product");
            }
        }

        //GET: Product/CheckOut
        [Authorize]
        [HttpGet]
        [Route("Product/CheckOut")]
        public async Task<IActionResult> CheckOut()
        {
            try
            {
                var user = await GetLoggedUser();
                var cartId = await _shopRepository.GetCartId(user.Id);
                await _shopRepository.UpdateCart(cartId);
                var model = new CheckOutModel
                {
                    Cart = await _shopRepository.GetCart(cartId)
                };

                if (model.Cart.Voucher != null) model.DiscountCode = model.Cart.Voucher.Discount.Code;

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy thông tin thanh toán");
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/CheckOut
        [Authorize]
        [HttpPost]
        [Route("Product/CheckOut")]
        public async Task<IActionResult> CheckOut(CheckOutModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin thanh toán không hợp lệ");
                return RedirectToAction("ShowCart", "Product");
            }

            try
            {
                await _shopRepository.CreateOrder(model.Cart.CartId, model.Address, model.Note);
                await _shopRepository.EmptyCart(model.Cart.CartId);
                _notyf.Success("Đặt hàng thành công");
                return RedirectToAction("Index", "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi đặt hàng");
                return RedirectToAction("Index", "Product");
            }
        }

        private async Task<Users> GetLoggedUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}