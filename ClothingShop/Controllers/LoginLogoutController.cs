using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingShop.BusinessLogic.Repositories;
using ClothingShop.Entity.Models;
using System.Security.Cryptography;
using System.Text;
using ClothingShop.Entity.Data;

namespace ClothingShop.Controllers
{
    public class LoginLogoutController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private readonly ShopContext _db;

        public LoginLogoutController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public ActionResult Index()
        {
            
                return RedirectToAction("Login");
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.UserName
                };
                var hash_pass = GetMD5(model.Password);
                var result = await userManager.CreateAsync(user, hash_pass);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");

                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var hash_pass = GetMD5(model.Password);
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.UserName,
                    
                };
                
                var data = _db.Users.Where(s => s.Email.Equals(user.UserName) && s.PasswordHash.Equals(hash_pass)).ToList();
                if (data.Count() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
               
            }
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            
            return RedirectToAction("Login");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}
