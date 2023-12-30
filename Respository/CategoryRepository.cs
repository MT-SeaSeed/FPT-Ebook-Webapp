using FPT_Ebook.Models;

namespace FPT_Ebook.Respository
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly FptebookContext _context;
        public CategoryRespository( FptebookContext context )
        {
            _context = context;
        }
        public Category Add(Category Category)
        {
            _context.Categories.Add( Category );
            _context.SaveChanges();
            return Category;
        }

        public Category Delete(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public Category Get(int CategoryID)
        {
            return _context.Categories.Find(CategoryID);
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _context.Categories;
        }

        public Category Update(Category Category)
        {
            _context.Update(Category);
            _context.SaveChanges();
            return Category;
        }
    }
}
