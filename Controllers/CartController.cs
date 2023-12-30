using FPT_Ebook.Helper;
using FPT_Ebook.Models;
using FPT_Ebook.Models.ProductModels;
using FPT_Ebook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FPT_Ebook.Controllers
{
    public class CartController : Controller
    {
        private FptebookContext DB;

        public CartController(FptebookContext context)
        {
            DB = context;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddtoCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.BookId == id);
            if (item == null)
            {
                var book = DB.Books.SingleOrDefault(p => p.BookId == id);
                if (book == null)
                {
                    TempData["Message"] = "BookID not found";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price ?? 0,
                    Quantity = quantity,
                    ImagePath = book.ImagePath ?? string.Empty
                };
                cart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, cart);
            return RedirectToAction("index");

        }
        public IActionResult RemoveCart(int id)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.BookId == id);
            if(item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }
            return RedirectToAction("index");
        }
        [Authorize]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return RedirectToAction("/");
            }
            return View(Cart);
        }

    }
}
