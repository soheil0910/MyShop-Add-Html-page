using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.Data;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZarinpalSandbox;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyShopContext _myShopContext;


       




        public HomeController(ILogger<HomeController> logger, MyShopContext myShopContext)
        {
            _logger = logger;
            _myShopContext = myShopContext;
        }
        [Authorize]
        public IActionResult AddToCart(int ItemId)
        {

            var produc = _myShopContext.Products
                .Include(i => i.Item)
                .SingleOrDefault(i => i.ItemId == ItemId);
            if (produc != null)
            {

                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _myShopContext.Orders.FirstOrDefault(c => c.UserId == userId && !c.IsFinaly);
                if (order != null)
                {
                    var orderDitile = _myShopContext.OrderDetails.FirstOrDefault(d =>
                        d.ProductId == produc.Id && d.OrderId == order.OrderId);
                    if (orderDitile != null)
                    {
                        orderDitile.Count += 1;
                    }
                    else
                    {
                        _myShopContext.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = produc.Id,
                            Price = produc.Item.Price,
                            Count = 1

                        });

                    }


                }
                else
                {
                    order = new Order()
                    {
                        IsFinaly = false,
                        UserId = userId,
                        CreateDate = DateTime.Now
                    };
                    _myShopContext.Orders.Add(order);
                    _myShopContext.SaveChanges();
                    _myShopContext.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = produc.Id,
                        Price = produc.Item.Price,
                        Count = 1

                    });
                }


                _myShopContext.SaveChanges();


            }





            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public IActionResult ShowCart()
        {

           
            


            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _myShopContext.Orders
                .Where(c => c.UserId == userId && !c.IsFinaly)
                .Include(i => i.OrderDetails)
                .ThenInclude(c => c.Product).FirstOrDefault();

            return View(order);
        }



        public IActionResult RemoveCart(int Id)
        {
            var objr = _myShopContext.OrderDetails.Find(Id);
            if (objr.Count == 1)
            {
                _myShopContext.OrderDetails.Remove(objr);

            }
            else
            {
                objr.Count -= 1;
            }

            _myShopContext.SaveChanges();
            return RedirectToAction("ShowCart");

        }



        public IActionResult Description(int Id)
        {
            //var produc = _myShopContext.Products.FirstOrDefault(p => p.Id == Id);
            //var produc = _myShopContext.Products.Find(Id);




            var produc = _myShopContext.Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == Id);






            if (produc == null)
            {
                return NotFound();

            }

            var category = _myShopContext.Products
                .Where(p => p.Id == Id)
                .SelectMany(a => a.categoryToProduct)
                .Select(b => b.category).ToList();



            var DataView = new DetailsViewModel()
            {
                product = produc,
                categories = category
            };

            return View(DataView);
        }

        public IActionResult Index()
        {

            var DataSql = _myShopContext.Products.Include(p=>p.Item).ToList();
            
            return View(DataSql);
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize]
        public IActionResult Pyment()
        {
            int UserId=int.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order=_myShopContext.Orders.Include(c=>c.OrderDetails)
                .FirstOrDefault(c=>c.UserId == UserId && !c.IsFinaly);
            if (order == null)
            {
                return NotFound();
            }


            var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "https://soheil0910.ir/Home/OnlinePayment/" + order.OrderId, "soheil0910line@gmail.com", "09106778366");
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }





        }


        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = _myShopContext.Orders.Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderId == id);
                var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinaly = true;
                    _myShopContext.Orders.Update(order);
                    _myShopContext.SaveChanges();
                    ViewBag.code = res.RefId;
                    return View();
                }
            }

            return RedirectToAction("Error");
        }


        


    }
}
