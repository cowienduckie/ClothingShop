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
    }
}
