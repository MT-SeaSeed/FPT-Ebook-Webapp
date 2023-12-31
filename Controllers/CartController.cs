using FPT_Ebook.Helper;
using FPT_Ebook.Models;
using FPT_Ebook.Models.Authentication;
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
        [Authentication]
        public IActionResult Index()
        {
            return View(Cart);
        }
        [Authentication]
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
        [Authentication]
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
        [Authentication]
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return RedirectToAction("/");
            }
            return View(Cart);
        }

        [HttpPost]
        public IActionResult ProcessCheckout()
        {
            if (Cart.Count == 0)
            {
                return Json(new { success = false, message = "Cart is empty." });
            }

            // Additional logic to handle the form submission, e.g., save order to the database

            // Clear the cart after a successful order
            HttpContext.Session.Remove(MySetting.CART_KEY);

            // Return a JSON response with success and message
            return Json(new { success = true, message = "Order placed successfully!" });
        }

    }
}
