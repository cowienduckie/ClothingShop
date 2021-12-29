using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Models;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ClothingShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IShopRepository _shopRepository;

        public AdminController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
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
                return RedirectToAction("ProductList");
            }
        }

        //GET: Admin/ProductDetails
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null) return RedirectToAction(nameof(ProductList));
            var product = await _shopRepository.GetProductDetails(id);
            if (product == null) return NotFound();

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
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _shopRepository.CreateProduct(model);
                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(model);
            }
        }

        //GET: Admin/EditProduct
        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) return RedirectToAction(nameof(ProductList));
            var product = await _shopRepository.GetProductDetails(id);
            if (product == null) return NotFound();

            return View(product);
        }

        //POST: Admin/ProductEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDetailModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var returnModel = await _shopRepository.EditProduct(model);

                if (returnModel == null)    
                    return View(model);

                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(model);
            }
        }

        //GET: Admin/DeleteProduct
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return RedirectToAction(nameof(ProductList));
            var product = await _shopRepository.GetProductDetails(id);
            if (product == null) return NotFound();

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
                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _shopRepository.CreateCategory(model);
                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(model);
            }
        }

        //GET: Admin/EditCategory
        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null) return RedirectToAction(nameof(CategoryList));
            var category = await _shopRepository.GetCategoryDetails(id);
            if (category == null) return NotFound();

            return View(category);
        }

        //POST: Admin/EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var returnModel = await _shopRepository.EditCategory(model);

                if (returnModel == null)
                    return View(model);

                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
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
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _shopRepository.CreateDiscount(model);
                return RedirectToAction(nameof(DiscountList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(model);
            }
        }

        //GET: Admin/EditDiscount
        [HttpGet]
        public async Task<IActionResult> EditDiscount(int? id)
        {
            if (id == null) return RedirectToAction(nameof(DiscountList));
            var discount = await _shopRepository.GetDiscountDetails(id);
            if (discount == null) return NotFound();

            return View(discount);
        }

        //POST: Admin/EditDiscount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscount(DiscountModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var returnModel = await _shopRepository.EditDiscount(model);

                if (returnModel == null)
                    return View(model);

                return RedirectToAction(nameof(DiscountList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(model);
            }
        }

        //GET: Admin/DeleteDiscount
        [HttpGet]
        public async Task<IActionResult> DeleteDiscount(int? id)
        {
            if (id == null) return RedirectToAction(nameof(ProductList));
            try
            {
                await _shopRepository.DeleteDiscount(id.Value);
                return RedirectToAction(nameof(DiscountList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }

        //GET: Admin/CreateVoucher
        public async Task<IActionResult> CreateVoucher(int DiscountId, int VoucherNumber)
        {
            try
            {
                await _shopRepository.CreateVoucher(VoucherNumber, DiscountId);
                return RedirectToAction(nameof(EditDiscount), new {id = DiscountId} );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction(nameof(EditDiscount), new { id = DiscountId });
            }
        }

        //GET: Admin/CreateVoucher
        public async Task<IActionResult> DeleteVoucher(int VoucherId, int DiscountId)
        {
            try
            {
                await _shopRepository.DeleteVoucher(VoucherId);
                return RedirectToAction(nameof(EditDiscount), new { id = DiscountId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction(nameof(EditDiscount), new { id = DiscountId });
            }
        }

        //GET: Admin/CreateVoucher
        public async Task<IActionResult> DeleteAllVoucher(int DiscountId)
        {
            try
            {
                await _shopRepository.DeleteAllVoucher(DiscountId);
                return RedirectToAction(nameof(EditDiscount), new { id = DiscountId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction(nameof(EditDiscount), new { id = DiscountId });
            }
        }
    }
}