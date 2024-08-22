using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Data.Repositories;
using System.Linq;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        private MyShopContext _myShopContext;
       



        public ProductController(MyShopContext myShopContext)
        {
            _myShopContext = myShopContext;
        }



        [Route("Group/{Id}/{Name}")]
        public IActionResult ShowProductByGroupId(int Id, string Name)
        {
            ViewData["Name"] = Name;
            var products = _myShopContext.CategoryToProducts
                .Where(c => c.CategoryId == Id)
                .Include(p => p.product)
                .ThenInclude(p=>p.Item)
                .Select(p => p.product)
                .ToList();
            return View(products);
        }








    }
}