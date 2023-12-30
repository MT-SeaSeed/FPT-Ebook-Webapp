using FPT_Ebook.Helper;
using FPT_Ebook.Models;
using FPT_Ebook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Ebook.ViewComponents
{
    public class CartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                Quatity = cart.Sum(p=>p.Quantity),
                Total = cart.Sum(p => p.TotalPrice)
            });
        }
    }
}
