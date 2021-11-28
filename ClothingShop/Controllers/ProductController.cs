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
        public IActionResult Index(string name, string sort, int? pageNumber, int? pageSize)
        {
            try
            {
                pageSize ??= 9; //9 items per page
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
                return View();
            }
        }
    }
}
