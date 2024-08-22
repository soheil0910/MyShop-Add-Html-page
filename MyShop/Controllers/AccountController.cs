using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using MyShop.Data.Repositories;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {


        private IUserRepository _user;

        public AccountController(IUserRepository user)
        {
            _user = user;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            //if (_user.IsExistUserByEmail(register.Email.ToLower()))
            //{
            //    ModelState.AddModelError("Email", "این ایمیل قبلا ثبت نام کرده است");
            //    return View(register);
            //}

            Users user = new Users()
            {
                Email = register.Email.ToLower(),
                Password = register.Password,
                IsAdmin = false,
                RegisterDate = DateTime.Now,

            };


            _user.addUser(user);


            return View("SuccessRegister", register);
        }


        public IActionResult VerifyEmail(string Email)
        {
            if (_user.IsExistUserByEmail(Email.ToLower()))
            {
                return Json($"این ایمیل{Email} قبلا ثبت نام کرده است");
            }
            return Json(true);
        }


        #endregion



        #region Login


        public IActionResult Login()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _user.GetUserForLogin(login.Email.ToLower(), login.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                


            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }




        #endregion

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
