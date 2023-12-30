using FPT_Ebook.Models;

namespace FPT_Ebook.Respository
{
    public interface ICategoryRepository
    {
        Category Add(Category Category);
        Category Update(Category Category);
        Category Delete(int CategoryID);
        Category Get(int CategoryID);
        IEnumerable<Category> GetAllCategory();
    }
}
