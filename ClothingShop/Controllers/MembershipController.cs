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
    public class MembershipController : Controller
    {
        private readonly IShopRepository _shopRepository;
        private readonly UserManager<Users> _userManager;

        public MembershipController(IShopRepository shopRepository,
                                    UserManager<Users> userManager)
        {
            _shopRepository = shopRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetLoggedUser();
            var model = new MembershipModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                TotalPoint = user.TotalPoint,
                Rank = _shopRepository.GetRank(user.RankId.Value),
                RankList = _shopRepository.GetAllRanks(),
                VoucherList = _shopRepository.GetVoucherListByUser(user.Id, 2)
            };

            return View(model);
        }

        [Route("VoucherList")]
        [HttpGet]
        public async Task<IActionResult> VoucherList()
        {
            var user = await GetLoggedUser();
            var model = _shopRepository.GetVoucherListByUser(user.Id);

            return PartialView(model);
        }

        public async Task<IActionResult> RedeemVoucher(int VoucherId)
        {
            var user = await GetLoggedUser();

            await _shopRepository.RedeemVoucher(user.Id, VoucherId);

            return RedirectToAction("ShowCart", "Product");
        }

        [HttpGet]
        [Route("CancelApplyingVoucher")]
        public async Task<IActionResult> CancelApplyingVoucher()
        {
            try
            {
                var user = await GetLoggedUser();

                await _shopRepository.CancelApplyingVoucher(user.Id);

                return RedirectToAction("ShowCart", "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Product");
            }
        }

        private async Task<Users> GetLoggedUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}