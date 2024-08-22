using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MyShopContext _context;

        public IEnumerable<Product> product { get; set; }
        
        public IndexModel(MyShopContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            product = _context.Products.Include(c=>c.Item);
        
        }
    }
}
