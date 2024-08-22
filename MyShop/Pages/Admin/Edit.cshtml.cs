using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyShop.Pages.Admin
{
    public class EditModel : PageModel
    {

        private MyShopContext _context { get; set; }

        public EditModel(MyShopContext context)
        {
            _context = context;

        }


        [BindProperty]
        public AddEditProductViewModel ProductPage { get; set; }

        [BindProperty]
        public List<int> GroupSelect { get; set; }






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

            ProductPage.Category=_context.Categories.ToList();

            GroupSelect=_context.CategoryToProducts.Where(c=>c.ProductId == Id).Select(x=>x.CategoryId).ToList();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                 return Page();
            

            var product = _context.Products.Find(ProductPage.Id);
            var item = _context.Items.First(p => p.Id == product.ItemId);

            
                item.Price = ProductPage.Price;
                item.QuantityInStock = ProductPage.QuantityInStock;
           
            //_context.Items.Add(item);



    
            product.Name = ProductPage.Name;
            product.Description = ProductPage.Description;
            product.Item = item;

           
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




            _context.CategoryToProducts.Where(c => c.ProductId == ProductPage.Id).ToList()
               .ForEach(g => _context.CategoryToProducts.Remove(g));

            if (GroupSelect.Any() && GroupSelect.Count > 0)
            {
                foreach (int gr in GroupSelect)
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct()
                    {
                        CategoryId = gr,
                        ProductId = ProductPage.Id
                    });
                }

                _context.SaveChanges();
            }











            return RedirectToPage("Index");
        }








    }
}
