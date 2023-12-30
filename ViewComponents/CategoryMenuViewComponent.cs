using Microsoft.AspNetCore.Mvc;
using FPT_Ebook.Models;
using FPT_Ebook.Respository;
namespace FPT_Ebook.ViewComponents
{
    public class CategoryMenuViewComponent: ViewComponent
    {
        private readonly ICategoryRepository _category;

        public CategoryMenuViewComponent(ICategoryRepository categoryRespository)
        {
            _category = categoryRespository;
        }
        public IViewComponentResult Invoke()
        {
            var MenuData = _category.GetAllCategory().OrderBy(x => x.CategoryName);
            return View(MenuData);
        }

    }
}
