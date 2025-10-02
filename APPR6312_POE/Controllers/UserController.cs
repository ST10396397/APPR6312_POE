using APPR6312_POE.Data;
using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace APPR6312_POE.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.HashedPassword == hashedPassword);

            if (user != null)
            {
                // TODO: Add session/cookie authentication
                TempData["Message"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving
                newUser.HashedPassword = HashPassword(newUser.HashedPassword);

                _context.Users.Add(newUser);
                _context.SaveChanges();

                TempData["Message"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            return View(newUser);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }      
    }
}
