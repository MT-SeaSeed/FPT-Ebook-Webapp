using Microsoft.AspNetCore.Mvc;
using FPT_Ebook.Models;
using FPT_Ebook.Respository;
namespace FPT_Ebook.ViewComponents
{
    public class CategoryFeaturedViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _category;

        public CategoryFeaturedViewComponent(ICategoryRepository categoryRespository)
        {
            _category = categoryRespository;
        }
        public IViewComponentResult Invoke()
        {
            var featuredData = _category.GetAllCategory().OrderBy(x => x.CategoryName);
            return View(featuredData);
        }

    }
}
