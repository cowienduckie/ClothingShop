using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IShopRepository _shopRepository;

        public AdminController(IShopRepository shopRepository, INotyfService notyf)
        {
            _shopRepository = shopRepository;
            _notyf = notyf;
        }

        //GET: Admin/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _shopRepository.GetAllProduct();

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy danh sách sản phẩm");
                return View();
            }
        }

        //GET: Admin/ProductList
        [HttpGet]
        [Route("Admin/ProductList")]
        public IActionResult ProductList(string name, string sort, int? category, int? pageNumber, int? pageSize)
        {
            try
            {
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
                return RedirectToAction("ProductList");
            }
        }

        //GET: Admin/ProductDetails
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã sản phẩm");
                return RedirectToAction(nameof(ProductList));
            }

            var product = await _shopRepository.GetProductDetails(id);

            if (product == null)
            {
                _notyf.Error("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(ProductList));
            }

            return View(product);
        }

        //GET: Admin/CreateProduct
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var model = await _shopRepository.GetBlankProductDetailModel();

            return View(model);
        }

        //POST: Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin sản phẩm không hợp lệ");
                return View(model);
            }

            try
            {
                await _shopRepository.CreateProduct(model);
                _notyf.Success("Thêm sản phẩm thành công");
                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi thêm sản phẩm");
                return View(model);
            }
        }

        //GET: Admin/EditProduct
        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã sản phẩm");
                return RedirectToAction(nameof(ProductList));
            }

            var product = await _shopRepository.GetProductDetails(id);

            if (product == null)
            {
                _notyf.Error("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(ProductList));
            }

            return View(product);
        }

        //POST: Admin/ProductEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin sản phẩm không hợp lệ");
                return View(model);
            }

            try
            {
                var returnModel = await _shopRepository.EditProduct(model);

                if (returnModel == null)
                {
                    _notyf.Error("Thông tin sản phẩm không hợp lệ");
                    return View(model);
                }

                _notyf.Success("Chỉnh sửa sản phẩm thành công");
                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi thay đổi sản phẩm");
                return View(model);
            }
        }

        //GET: Admin/DeleteProduct
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã sản phẩm");
                return RedirectToAction(nameof(ProductList));
            }

            var product = await _shopRepository.GetProductDetails(id);

            if (product == null)
            {
                _notyf.Error("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(ProductList));
            }

            return View(product);
        }

        //POST: Admin/DeleteProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _shopRepository.DeleteProduct(id);
                _notyf.Success("Xóa sản phẩm thành công");
                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi xóa sản phẩm");
                return View();
            }
        }

        //GET: Admin/CategoryList
        [HttpGet]
        public IActionResult CategoryList(int? pageNumber, int? pageSize)
        {
            try
            {
                var model = _shopRepository.GetCategoryList(pageNumber, pageSize);

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                _notyf.Error("Có lỗi xảy ra khi lấy danh sách danh mục");
                return View();
            }
        }

        //GET: Admin/CreateCategory
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new CategoryModel());
        }

        //POST: Admin/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin danh mục không hợp lệ");
                return View(model);
            }

            try
            {
                await _shopRepository.CreateCategory(model);
                _notyf.Success("Thêm danh mục thành công");
                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi thêm danh mục");
                return View(model);
            }
        }

        //GET: Admin/EditCategory
        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã danh mục");
                return RedirectToAction(nameof(CategoryList));
            }

            var category = await _shopRepository.GetCategoryDetails(id);

            if (category == null)
            {
                _notyf.Error("Không tìm thấy danh mục");
                return RedirectToAction(nameof(CategoryList));
            }

            return View(category);
        }

        //POST: Admin/EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin danh mục không hợp lệ");
                return View(model);
            }

            try
            {
                var returnModel = await _shopRepository.EditCategory(model);

                if (returnModel == null)
                {
                    _notyf.Error("Thông tin danh mục không hợp lệ");
                    return View(model);
                }

                _notyf.Success("Chỉnh sửa danh mục thành công");
                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi chỉnh sửa danh mục");
                return View(model);
            }
        }

        //GET: Admin/DeleteCategory
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return RedirectToAction(nameof(ProductList));
            try
            {
                await _shopRepository.DeleteCategory(id.Value);
                _notyf.Success("Xóa danh mục thành công");
                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi xóa danh mục");
                return RedirectToAction(nameof(CategoryList));
            }
        }

        //GET: Admin/DiscountList
        public IActionResult DiscountList(string name, int? pageNumber, int? pageSize)
        {
            try
            {
                var model = _shopRepository.GetDiscountList(name, pageNumber, pageSize);

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi tải danh sách khuyến mãi");
                return View();
            }
        }

        //GET: Admin/CreateDiscount
        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View(new DiscountModel());
        }

        //POST: Admin/CreateDiscount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDiscount(DiscountModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin khuyến mãi không hợp lệ");
                return View(model);
            }

            try
            {
                await _shopRepository.CreateDiscount(model);
                _notyf.Success("Thêm khuyến mãi thành công");
                return RedirectToAction(nameof(DiscountList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                _notyf.Error("Có lỗi xảy ra thêm khuyến mãi");
                return View(model);
            }
        }

        //GET: Admin/EditDiscount
        [HttpGet]
        public async Task<IActionResult> EditDiscount(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã khuyến mãi");
                return RedirectToAction(nameof(DiscountList));
            }

            var discount = await _shopRepository.GetDiscountDetails(id);

            if (discount == null)
            {
                _notyf.Error("Không tìm thấy khuyến mãi");
                return RedirectToAction(nameof(DiscountList));
            }

            return View(discount);
        }

        //POST: Admin/EditDiscount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscount(DiscountModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin khuyến mãi không hợp lệ");
                return View(model);
            }

            try
            {
                var returnModel = await _shopRepository.EditDiscount(model);

                if (returnModel == null)
                {
                    _notyf.Error("Thông tin khuyến mãi không hợp lệ");
                    return View(model);
                }

                _notyf.Success("Chỉnh sửa khuyến mãi thành công");
                return RedirectToAction(nameof(DiscountList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi chỉnh sửa khuyến mãi");
                return View(model);
            }
        }

        //GET: Admin/DeleteDiscount
        [HttpGet]
        public async Task<IActionResult> DeleteDiscount(int? id)
        {
            if (id == null)
            {
                _notyf.Error("Không có mã khuyến mãi");
                return RedirectToAction(nameof(DiscountList));
            }

            try
            {
                await _shopRepository.DeleteDiscount(id.Value);
                _notyf.Success("Xoá khuyến mãi thành công");
                return RedirectToAction(nameof(DiscountList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi xoá khuyến mãi");
                return RedirectToAction(nameof(DiscountList));
            }
        }

        //GET: Admin/CreateVoucher
        public async Task<IActionResult> CreateVoucher(int DiscountId, int VoucherNumber)
        {
            try
            {
                await _shopRepository.CreateVoucher(VoucherNumber, DiscountId);
                _notyf.Success("Tạo phiếu ưu đãi thành công");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi tạo phiếu ưu đãi");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
        }

        public async Task<IActionResult> SendVoucherToAllUser(int DiscountId)
        {
            try
            {
                await _shopRepository.SendVoucherToAllUser(DiscountId);
                _notyf.Success("Gửi phiếu ưu đãi thành công");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi gửi phiếu ưu đãi");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
        }

        public async Task<IActionResult> SendVoucher(int DiscountId, string SendVoucherUserName)
        {
            try
            {
                await _shopRepository.SendVoucher(DiscountId, SendVoucherUserName);
                _notyf.Success("Gửi phiếu ưu đãi thành công");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi gửi phiếu ưu đãi");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
        }

        //GET: Admin/CreateVoucher
        public async Task<IActionResult> DeleteVoucher(int VoucherId, int DiscountId)
        {
            try
            {
                await _shopRepository.DeleteVoucher(VoucherId);
                _notyf.Success("Xoá phiếu ưu đãi thành công");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi xoá phiếu ưu đãi");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
        }

        //POST: Admin/CreateVoucher
        public async Task<IActionResult> DeleteAllVoucher(int DiscountId)
        {
            try
            {
                await _shopRepository.DeleteAllVoucher(DiscountId);
                _notyf.Success("Xoá phiếu ưu đãi thành công");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi xoá phiếu ưu đãi");
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId});
            }
        }

        //GET: Admin/OrderList
        public IActionResult OrderList(int? orderId, string status, int? pageNumber, int? pageSize)
        {
            try
            {
                var model = _shopRepository.GetOrderList(orderId, status, pageNumber, pageSize);

                //View bag
                if (orderId != null) ViewBag.OrderId = orderId;
                if (status != null) ViewBag.Status = status;

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy dánh sách đơn hàng");
                return RedirectToAction("ProductList");
            }
        }

        public async Task<IActionResult> OrderDetails(int orderId)
        {
            try
            {
                var model = await _shopRepository.GetOrder(orderId);

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Có lỗi xảy ra khi lấy chi tiết đơn hàng");
                return RedirectToAction("OrderList");
            }
        }

        public async Task<IActionResult> AcceptOrder(int orderId)
        {
            try
            {
                await _shopRepository.AcceptOrder(orderId);
                _notyf.Success("Chấp nhận đơn hàng thành công");
                return RedirectToAction("OrderList");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Chấp nhận đơn hàng thất bại");
                return RedirectToAction("ProductList");
            }
        }

        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                await _shopRepository.CancelOrder(orderId);
                _notyf.Warning("Huỷ đơn hàng thành công");
                return RedirectToAction("OrderList");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _notyf.Error("Huỷ đơn hàng thất bại");
                return RedirectToAction("ProductList");
            }
        }
    }
}