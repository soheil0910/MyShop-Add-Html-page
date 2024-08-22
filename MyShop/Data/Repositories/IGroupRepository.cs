using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Data.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ShowGroupViewModel> GetGroupForShow();
    }

    public class GroupRepository : IGroupRepository
    {
        private MyShopContext _context;
             
        public GroupRepository(MyShopContext context)
        {
            _context = context;
        }




        public IEnumerable<Category> GetAllCategories()
        {
           return _context.Categories;
        }



        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
         return   _context.Categories
                .Select(c => new ShowGroupViewModel()
                {
                    GroupId = c.Id,
                    Name = c.Name,
                    ProductCount = c.categoryToProduct.Count(q => q.CategoryId == c.Id),
                });
        }
    }
}
