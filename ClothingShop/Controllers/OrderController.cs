using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClothingShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IShopRepository _shopRepository;
        private readonly UserManager<Users> _userManager;

        public OrderController(IShopRepository shopRepository,
                               UserManager<Users> userManager)
        {
            _shopRepository = shopRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> OrderHistory(int? pageNumber, int? pageSize)
        {
            try
            {
                var user = await GetLoggedUser();
                var model = _shopRepository.GetOrderHistory(user.Id, pageNumber, pageSize);

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        public async Task<IActionResult> Details(int orderId)
        {
            try
            {
                var model = await _shopRepository.GetOrder(orderId);

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index");
            }
        }

        private async Task<Users> GetLoggedUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
