using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyShop.Models
{
    public class AddEditProductViewModel
    {

        public int Id { get; set; }
        [Display(Name = "قیمت")]
        [Required]
     
        public decimal Price { get; set; }
        [Display(Name = "تعداد")]
        [Required]

        public int QuantityInStock { get; set; }
        [Display(Name = "نام")]
        [Required]

        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        [Required]

        public string Description { get; set; }
        [Display(Name = "عکس")]
        

        public IFormFile Picture { get; set; }

        public List<Category> Category { get; set; }

    }
}
