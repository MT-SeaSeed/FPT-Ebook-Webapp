using FPT_Ebook.Models;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Ebook.Controllers
{
    public class AccessController : Controller
    {
        FptebookContext db = new FptebookContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var u = db.Users.FirstOrDefault(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
                if (u != null)
                {
                    HttpContext.Session.SetString("Email", u.Email.ToString());

                    if (u.Role == "Admin")
                    {
                        return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin") });
                    }
                    else
                    {
                        return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                    }
                }
            }

            return Json(new { success = false, message = "Invalid email or password." });
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = db.Users.FirstOrDefault(x => x.Email == user.Email);
                    if (existingUser == null)
                    {
                        user.UserId = GenerateUniqueUserId();
                        user.Role = "Customer";

                        db.Users.Add(user);
                        db.SaveChanges();

                        HttpContext.Session.SetString("Email", user.Email.ToString());

                        return RedirectToAction("Login", "Access");
                    }
                    else
                    {
                        // Email is already in use
                        return Json(new { success = false, message = "Email is already in use." });
                    }
                }
                return View(user);
            }
            catch (Exception)
            {
                // Log or handle the exception appropriately
                return Json(new { success = false, message = "Error occurred while registering. Please try again." });
            }
        }

        private int GenerateUniqueUserId()
        {
            Random random = new Random();
            int newUserId;
            do
            {
                newUserId = random.Next(100000, 999999); 
            } while (db.Users.Any(x => x.UserId == newUserId));

            return newUserId;
        }
    }

}