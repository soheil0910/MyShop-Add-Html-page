using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Pages.Admin
{
    public class DeleteModel : PageModel
    {





        private MyShopContext _context { get; set; }

        public DeleteModel(MyShopContext context)
        {
            _context = context;

        }


        [BindProperty]
        public AddEditProductViewModel ProductPage { get; set; }

        public void OnGet(int Id)
        {
            ProductPage = _context.Products.Include(c => c.Item).Where(c => c.Id == Id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.Id,
                    Description = s.Description,
                    Name = s.Name,
                    Price = s.Item.Price,
                    QuantityInStock = s.Item.QuantityInStock,
                    

                }).FirstOrDefault();



        }


        public IActionResult OnPost()
        {
            


            var product = _context.Products.Find(ProductPage.Id);
            var item = _context.Items.First(p => p.Id == product.ItemId);


            

            _context.Products.Remove(product);
            _context.Items.Remove(item);


           


            _context.SaveChanges();


            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                product.Id + ".png");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }



            return RedirectToPage("Index");

        }

    }
}
