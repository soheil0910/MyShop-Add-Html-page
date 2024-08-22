using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Html_login
    {
       
            [Required(ErrorMessage = "لطفآ {0} را وارد کنید")]
            [MaxLength(300)]
            [EmailAddress(ErrorMessage = "باید {0} به درستی وارد کنید")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }


            [Required(ErrorMessage = "لطفآ {0} را وارد کنید")]
            [MaxLength(50)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور ")]
            public string Password { get; set; }


            [Display(Name = "مرا به خاطر بسپار ")]
            public bool RememberMe { get; set; }





            public List<Product> products { get; set; }






















    }
}
