using System.Collections.Generic;

namespace MyShop.Models
{
    public class Product
    {

        //public Product()
        //{
        //    Categories  = new List<Category>();
        //}


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }

        //public List<Category> Categories { get; set; }

        public Item Item { get; set; }




        public ICollection<CategoryToProduct> categoryToProduct { get; set; }


        public List<OrderDetail> OrderDetails { get; set; }
    }
}
