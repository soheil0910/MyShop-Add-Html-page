using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Data.Repositories;
using MyShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Components
{
    public class ProductGroupsComponent : ViewComponent
    {
        private IGroupRepository _groupRepository;

        public ProductGroupsComponent(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var categories = _context.Categories
            //    .Select(c => new ShowGroupViewModel()
            //    {
            //        GroupId = c.Id,
            //        Name = c.Name,
            //        ProductCount = c.categoryToProduct.Count(q => q.CategoryId==c.Id),
            //    });
            return View("/Views/Components/ProductGroupsComponent.cshtml", _groupRepository.GetGroupForShow());

            //var obj = _context.Categories.Include(c => c.categoryToProduct).ToList();
            //return View("/Views/Components/ProductGroupsComponent.cshtml", obj);
        }
    }
}
