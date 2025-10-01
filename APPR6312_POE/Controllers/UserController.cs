using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Handle login logic here
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save user to the database (add this later)
                TempData["Message"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            return View(newUser);
        }
    }
}
