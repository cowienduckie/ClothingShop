using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IShopRepository _shopRepository;
        private readonly UserManager<Users> _userManager;

        public OrderController(IShopRepository shopRepository,
            UserManager<Users> userManager,
            INotyfService notyf)
        {
            _shopRepository = shopRepository;
            _userManager = userManager;
            _notyf = notyf;
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
                _notyf.Error("Xảy ra lỗi khi lấy lịch sử giao dịch");
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
                _notyf.Error("Xảy ra lỗi khi lấy chi tiết giao dịch");
                return RedirectToAction("Index", "Home");
            }
        }

        private async Task<Users> GetLoggedUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}