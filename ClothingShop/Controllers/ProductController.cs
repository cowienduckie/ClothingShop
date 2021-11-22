using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
