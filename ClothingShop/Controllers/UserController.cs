using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Roles> roleManager;

        public UserController(UserManager<Users> userManager, RoleManager<Roles> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        //GET: User
        [HttpGet]
        [Route("User")]
        public IActionResult Index(int? pageNumber, int? pageSize)
        {
            var users = userManager.Users.AsQueryable();
            var PageSize = pageSize ?? 20;
            var PageNumber = pageNumber ?? 1;
            var Total = users.Count();

            var model = new PaginationModel<UserViewModel>
            {
                ItemList = new List<UserViewModel>(),
                Total = Total,
                PageSize = PageSize,
                PageNumber = PageNumber
            };

            users.Skip(PageSize * (PageNumber - 1))
                 .Take(PageSize)
                 .ToList()
                 .ForEach(u =>
                 {
                     model.ItemList.Add(new UserViewModel
                     {
                         Id = u.Id,
                         Email = u.Email,
                         UserName = u.UserName,
                         RoleNames = (List<string>)userManager.GetRolesAsync(u).Result
                     });
                 });

            return View(model);
        }

        //GET: User/Create
        [HttpGet]
        [Route("User/Create")]
        public IActionResult Create()
        {
            UserDetailModel model = new UserDetailModel
            {
                Roles = roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList()
            };

            return View(model);
        }

        //POST: User/Create
        [HttpPost]
        [Route("User/Create")]
        public async Task<IActionResult> Create(UserDetailModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                Users user = new Users
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    Roles role = await roleManager.FindByIdAsync(model.RoleId).ConfigureAwait(false);

                    if (role != null)
                    {
                        IdentityResult roleResult = await userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(model);
            }
        }

        //GET: User/Edit
        [HttpGet]
        [Route("User/Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var model = new UserDetailModel
            {
                Roles = roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList()
            };

            if (!string.IsNullOrEmpty(id))
            {
                Users user = await userManager.FindByIdAsync(id).ConfigureAwait(false);

                if (user != null)
                {
                    model.Id = user.Id;
                    model.Email = user.Email;
                    model.RoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }

            return View(model);
        }

        //POST: User/Edit
        [HttpPost]
        [Route("User/Edit")]
        public async Task<IActionResult> Edit(UserDetailModel model)
        {
            model.Roles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            try
            {
                Users user = await userManager.FindByIdAsync(model.Id).ConfigureAwait(false);

                if (user != null)
                {
                    user.Email = model.Email;
                    string existingRole = userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await userManager.UpdateAsync(user).ConfigureAwait(false);

                    if (existingRoleId != model.RoleId)
                    {
                        IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRole).ConfigureAwait(false);
                        if (roleResult.Succeeded)
                        {
                            Roles applicationRole = await roleManager.FindByIdAsync(model.RoleId).ConfigureAwait(false);
                            if (applicationRole != null)
                            {
                                IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, applicationRole.Name).ConfigureAwait(false);
                            }
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(model);
            }
        }

        //GET: User/Delete
        [HttpGet]
        [Route("User/Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = new UserDetailModel
            {
                Roles = roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList()
            };

            if (!string.IsNullOrEmpty(id))
            {
                Users user = await userManager.FindByIdAsync(id).ConfigureAwait(false);

                if (user != null)
                {
                    model.Id = user.Id;
                    model.UserName = user.UserName;
                    model.Email = user.Email;
                    model.RoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }

            return View(model);
        }

        //POST: User/Delete
        [HttpPost]
        [Route("User/Delete")]
        public async Task<IActionResult> Delete(string id, IFormCollection form)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Users user = await userManager.FindByIdAsync(id).ConfigureAwait(false);
                if (user != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(user).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}