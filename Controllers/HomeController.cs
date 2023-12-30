using FPT_Ebook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;
using X.PagedList;
using FPT_Ebook.ViewModels;
using FPT_Ebook.Models.Authentication;

namespace FPT_Ebook.Controllers
{
    public class HomeController : Controller
    {
        FptebookContext DBbook = new FptebookContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page.HasValue && page > 0) ? page.Value : 1;
            var bookList = DBbook.Books.AsNoTracking().OrderBy(x => x.BookId);
            PagedList<Book> pagedList = new PagedList<Book>(bookList, pageNumber, pageSize);
            return View(pagedList);
        }
        public IActionResult Category(int categoryID, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page.HasValue && page > 0) ? page.Value : 1;
            var bookList = DBbook.Books.AsNoTracking().Where(x => x.CategoryId == categoryID).OrderBy(x => x.BookId);
            PagedList<Book> pagedList = new PagedList<Book>(bookList, pageNumber, pageSize);

            List<Book> CategoryList = DBbook.Books
                .Where(x => x.CategoryId == categoryID)
                .OrderBy(x => x.Title)
                .ToList();
            ViewBag.CategoryID = categoryID;
            return View(pagedList);
        }
        [Authentication]
        public IActionResult BookDetails(int BookID)
        {
            var data = DBbook.Books.SingleOrDefault(x => x.BookId == BookID);
            var result = new BookDetails
            {
                BookId = data.BookId,
                Title = data.Title,
                Author = data.Author,
                Price = data.Price,
                Description = data.Description ?? string.Empty,
                QuantityInStock = 1,
                ImagePath = data.ImagePath ?? string.Empty,
                CategoryId = data.CategoryId,
            };
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
