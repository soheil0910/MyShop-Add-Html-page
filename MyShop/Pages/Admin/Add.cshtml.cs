using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyShopContext _context { get; set; }

        public AddModel(MyShopContext context)
        {
            _context = context;

        }


        [BindProperty]
        public AddEditProductViewModel ProductPage { get; set; }



        [BindProperty]
        public List<int> backCategory { get; set; }
        public void OnGet()
        {
            ProductPage = new AddEditProductViewModel()
            {
                Category = _context.Categories.ToList()
            };
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Item item = new Item()
            {
                Price = ProductPage.Price,
                QuantityInStock = ProductPage.QuantityInStock
            };
            _context.Items.Add(item);
            _context.SaveChanges();


            Product product = new Product()
            {
                Name = ProductPage.Name,
                Description = ProductPage.Description,
                Item = item
            };
            _context.Products.Add(product);
            _context.SaveChanges();




            if (ProductPage.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    product.Id + Path.GetExtension(ProductPage.Picture.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ProductPage.Picture.CopyTo(stream);
                }
            }
            if (backCategory.Any() && backCategory.Count >0)
            {

                foreach (int ca in backCategory)               
                
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct
                    {

                        CategoryId = ca,
                        ProductId = product.Id,

                    });

                }
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
