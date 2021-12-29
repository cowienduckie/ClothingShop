using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingShop.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
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
                VoucherList = _shopRepository.GetVoucherListByUser(user.Id)
            };

            return View(model);
        }
    }
}
