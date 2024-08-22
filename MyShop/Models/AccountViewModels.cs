using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
   
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "لطفآ {0} را وارد کنید")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage ="باید {0} به درستی وارد کنید")]
        [Display(Name ="ایمیل")]
        [Remote("VerifyEmail", "Account")]
        public string Email { get; set; }


        [Required(ErrorMessage = "لطفآ {0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور ")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }



        [Required(ErrorMessage = "لطفآ {0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="با کلمه عبور متفاوت است")]
        [Display(Name = "تکرار کلمه عبور")]
        public string RePassword { get; set; }



    }

    public class LoginViewModel
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
    }
}
