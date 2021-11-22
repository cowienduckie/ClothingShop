using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult CreateItem()
        {
            return View();
        }
    }
}
