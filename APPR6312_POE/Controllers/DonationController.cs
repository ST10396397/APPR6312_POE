using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;
using APPR6312_POE.Data;

namespace APPR6312_POE.Controllers
{
    public class DonationController : Controller
    {
        private readonly AppDbContext _context;

        public DonationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Donation/
        public IActionResult Index()
        {
            return View("Donation");
        }

        // POST: /Donation/Donate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Donate(Donation model)
        {
            if (ModelState.IsValid)
            {
                _context.Donations.Add(model);
                _context.SaveChanges();

                return RedirectToAction("ThankYou");
            }

            return View("Donation", model);
        }

        // GET: /Donation/List
        public IActionResult List()
        {
            var donations = _context.Donations
                .OrderByDescending(d => d.DonationDate)
                .ToList();

            return View(donations);
        }

        // GET: /Donation/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
