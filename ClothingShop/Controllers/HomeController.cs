using System.Diagnostics;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClothingShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShopRepository _shopRepository;

        public HomeController(ILogger<HomeController> logger, IShopRepository shopRepository)
        {
            _logger = logger;
            _shopRepository = shopRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _shopRepository.GetAllCategories();
            ViewBag.HotItems = _shopRepository.GetProductList("", "", 19, null, null);
            ViewBag.NewItems = _shopRepository.GetProductList("", "", 20, null, null);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}