using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Models;
using ClothingShop.BusinessLogic.Repositories.Interfaces;

namespace ClothingShop.Controllers
{
    public class AdminController : Controller 
    {

        private readonly IShopRepository _context;

        public AdminController(IShopRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.GetAllProduct();

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var product = await _context.GetDetails(id);

            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,Name,image,Price,Discount,Dicription,CreateTime")] ProductDetailModels product)
        {
            if (ModelState.IsValid)
            {
                _ = _context.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var product = _context.GetDetails(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,image,Price,Discount,Dicription,CreateTime")] ProductDetailModels product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ =_context.EditProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
       

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _context.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
