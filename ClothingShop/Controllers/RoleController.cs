using System;
using System.Linq;
using System.Threading.Tasks;
using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Roles> roleManager;

        public RoleController(RoleManager<Roles> roleManager)
        {
            this.roleManager = roleManager;
        }

        //GET: Role
        [HttpGet]
        [Route("Role")]
        public IActionResult Index(int? pageNumber, int? pageSize)
        {
            try
            {
                var PageSize = pageSize ?? 20;
                var PageNumber = pageNumber ?? 1;
                var roles = roleManager.Roles.AsQueryable();

                var model = new PaginationModel<RoleDetailModel>
                {
                    ItemList = roles.Select(r => new RoleDetailModel
                    {
                        RoleName = r.Name,
                        Id = r.Id
                    }).ToList(),
                    Total = roles.Count(),
                    PageSize = PageSize,
                    PageNumber = PageNumber
                };

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }

        //GET: Role/CreateEdit
        [HttpGet]
        [Route("Role/CreateEdit")]
        public async Task<IActionResult> CreateEdit(string id)
        {
            var model = new RoleDetailModel();
            if (!string.IsNullOrEmpty(id))
            {
                var role = await roleManager.FindByIdAsync(id).ConfigureAwait(false);

                if (role != null)
                {
                    model.Id = role.Id;
                    model.RoleName = role.Name;
                }
            }

            return View(model);
        }

        //POST: Role/CreateEdit
        [HttpPost]
        [Route("Role/CreateEdit")]
        public async Task<IActionResult> CreateEdit(RoleDetailModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var id = model.Id;
                var isExist = !string.IsNullOrEmpty(id);

                var role = isExist ? await roleManager.FindByIdAsync(id) : new Roles();

                role.Name = model.RoleName;

                var roleResult = isExist
                    ? await roleManager.UpdateAsync(role).ConfigureAwait(false)
                    : await roleManager.CreateAsync(role).ConfigureAwait(false);
                if (roleResult.Succeeded) return RedirectToAction(nameof(Index));

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(model);
            }
        }

        //GET: Role/Delete
        [HttpGet]
        [Route("Role/Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var name = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                var role = await roleManager.FindByIdAsync(id).ConfigureAwait(false);
                if (role != null) return View(new RoleDetailModel {Id = role.Id, RoleName = role.Name});
            }

            return RedirectToAction(nameof(Index));
        }

        //POST: Role/Delete
        [HttpPost]
        [Route("Role/Delete")]
        public async Task<IActionResult> Delete(string id, IFormCollection form)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var role = await roleManager.FindByIdAsync(id).ConfigureAwait(false);
                if (role != null)
                {
                    var roleResult = roleManager.DeleteAsync(role).Result;
                    if (roleResult.Succeeded) return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }
    }
}