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
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IShopRepository _shopRepository;

        public AccountController(SignInManager<Users> signInManager,
                                 UserManager<Users> userManager,
                                 RoleManager<Roles> roleManager,
                                 IShopRepository shopRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _shopRepository = shopRepository;
            _roleManager = roleManager;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        //Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        //Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                Users user = new Users
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RankId = 2,
                    TotalPoint = 0
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    Roles role = await _roleManager.FindByNameAsync("User").ConfigureAwait(false);

                    if (role != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction(nameof(Login));
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

        //SignOff
        [HttpGet]
        [Route("Account/SignOff")]
        public async Task<IActionResult> SignOff()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}