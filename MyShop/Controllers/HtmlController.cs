using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MyShop.Data;
using MyShop.Data.Repositories;
using MyShop.Migrations;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class HtmlController : Controller
    {
        private MyShopContext _myShopContext;
        private IUserRepository _user;

        public HtmlController(MyShopContext myShopContext, IUserRepository user)
        {

            _user = user;
            _myShopContext = myShopContext;
        }


        public IActionResult Index()
        {
            Html_login H_login = new Html_login();

             H_login.products = _myShopContext.Products.Include(p=>p.Item).ToList();

            return View(H_login);
        }



        [HttpPost]
        public IActionResult Index(Html_login login)
        {
            login.products = _myShopContext.Products.ToList();
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _user.GetUserForLogin(login.Email.ToLower(), login.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                ViewBag.soa = 135;
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

            return Redirect("/html");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/html/Index");
        }














    }
}
