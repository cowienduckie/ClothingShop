using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Models;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using System;

namespace ClothingShop.Controllers
{
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
        public IActionResult ProductList(string name, string sort, int? pageNumber, int? pageSize)
        {
            try
            {
                var model = _shopRepository.GetProductList(name, sort, pageNumber, pageSize);

                //View bag
                if (name != null) ViewBag.Name = name;
                if (sort != null) ViewBag.Sort = sort;

                return View(model);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
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
    }
}