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
    public class ProductController : Controller
    {
        private readonly IShopRepository _shopRepository;

        public ProductController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
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
                return RedirectToAction("Index", "Home");
            }
        }

        //GET: Product/Details/id
        [HttpGet]
        [Route("Product/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));

            try
            {
                var model = await _shopRepository.GetProductDetails(id);

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/AddToCart
        [Authorize]
        [HttpPost]
        [Route("Product/AddToCart")]
        public async Task<IActionResult> AddToCart(int?ItemId)
        {
            if (ItemId == null) return RedirectToAction("Index", "Product");

            try
            {
                //Put item to cart

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/RemoveFromCart
        [Authorize]
        [HttpPost]
        [Route("Product/RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(int? ItemId)
        {
            if (ItemId == null) return RedirectToAction("Index", "Product");

            try
            {
                //Remove item from cart

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/UpdateCart
        [Authorize]
        [HttpPost]
        [Route("Product/UpdateCart")]
        public async Task<IActionResult> UpdateCart(int? ItemId)
        {
            if (ItemId == null) return RedirectToAction("Index", "Product");

            try
            {
                //Update cart for current changes

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/EmptyCart
        [Authorize]
        [HttpPost]
        [Route("Product/EmptyCart")]
        public async Task<IActionResult> EmptyCart(int? ItemId)
        {
            if (ItemId == null) return RedirectToAction("Index", "Product");

            try
            {
                //Empty cart for current changes

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                //Show cart of logged in user

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                //Show check out screen for confirmation
                //Get receiver's information (phone number, email, address, ... )

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        //POST: Product/CheckOut
        [Authorize]
        [HttpPost]
        [Route("Product/CheckOut")]
        public async Task<IActionResult> CheckOut(int param)
        {
            try
            {
                //Check out all items in cart

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }
    }
}
