using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClothingShop.Controllers
{
    public class OrderController : Controller
    {

        private readonly IShopRepository _shopRepository;

        public OrderController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public IActionResult Index()
        {
            try
            {
                var model = _shopRepository.GetAllOrder();

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }

        public async Task<IActionResult> Payment(OrderDetailModel model)
        {

            _ = _shopRepository.AddOrder(model);

            return View(model);
        }

        public async Task<IActionResult> GetOrderDetail(int? id)
        {            
            var order = await _shopRepository.GetOrderDetails(id);
            if (order == null) return NotFound();

            return View(order);
        }
    }
}
