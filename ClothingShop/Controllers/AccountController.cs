using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IShopRepository _shopRepository;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public AccountController(SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            IShopRepository shopRepository,
            INotyfService notyf)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _shopRepository = shopRepository;
            _roleManager = roleManager;
            _notyf = notyf;
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
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager
                .PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).ConfigureAwait(false);
            if (result.Succeeded)
            {
                _notyf.Success("Đăng nhập thành công");
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            _notyf.Error("Đăng nhập thất bại");
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
            if (!ModelState.IsValid)
            {
                _notyf.Error("Đăng ký thất bại");
                return View(model);
            }

            try
            {
                var user = new Users
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RankId = 2,
                    TotalPoint = 0
                };

                var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("User").ConfigureAwait(false);

                    if (role != null)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
                        if (roleResult.Succeeded)
                        {
                            _notyf.Success("Đăng ký thành công");
                            return RedirectToAction(nameof(Login));
                        }
                    }
                }

                _notyf.Error("Đăng ký thất bại");

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _notyf.Error("Có lỗi xảy ra khi đăng ký");
                return View(model);
            }
        }

        //SignOff
        [HttpGet]
        [Route("Account/SignOff")]
        public async Task<IActionResult> SignOff()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            _notyf.Success("Đăng xuất thành công");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}