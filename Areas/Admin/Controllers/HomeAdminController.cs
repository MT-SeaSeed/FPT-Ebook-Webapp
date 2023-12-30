using FPT_Ebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using FPT_Ebook.Models.Authentication;

namespace FPT_Ebook.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        FptebookContext DBBook = new FptebookContext();
        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
        [Route("BookList")]
        [Authentication]
        public IActionResult BookList()
        {
            var bookList = DBBook.Books.Include(b => b.Category).ToList();
            return View(bookList);
        }
        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(DBBook.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }
        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                DBBook.Books.Add(book);
                DBBook.SaveChanges();
                return RedirectToAction("BookList");
            }
            return View(book);
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int bookId)
        {
            var book = DBBook.Books.Include(b => b.Category).FirstOrDefault(b => b.BookId == bookId);
            ViewBag.CategoryId = new SelectList(DBBook.Categories.ToList(), "CategoryId", "CategoryName", book.CategoryId);

            return View(book);
        }
        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                DBBook.Entry(book).State = EntityState.Modified;
                DBBook.SaveChanges();
                return RedirectToAction("BookList", "HomeAdmin");
            }
            return View(book);
        }
        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(int BookId)
        {
            var bookToDelete = DBBook.Books.FirstOrDefault(b => b.BookId == BookId);
            DBBook.Remove(bookToDelete);
            DBBook.SaveChanges();
            return RedirectToAction("BookList", "HomeAdmin");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");

            return RedirectToAction("Index", "Home");
        }

    }
}
