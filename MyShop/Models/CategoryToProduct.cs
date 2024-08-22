using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class CategoryToProduct
    {
        
        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        //Navigation Property

        public Category category { get; set; }
        public Product product { get; set; }
    }
}
