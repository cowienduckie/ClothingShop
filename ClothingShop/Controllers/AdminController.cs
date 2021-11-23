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
        private readonly IShopRepository _shopRepository;

        public AdminController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _shopRepository.GetAllProduct();

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var product = await _shopRepository.GetDetails(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            var model = new ProductDetailModels();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Image,Price,Discount,Description")] ProductDetailModels product)
        {
            if (ModelState.IsValid)
            {
                await _shopRepository.CreateProduct(product);

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _shopRepository.GetDetails(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,image,Price,Discount,Description,CreateTime")] ProductDetailModels product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _shopRepository.EditProduct(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _shopRepository.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }
    }
}