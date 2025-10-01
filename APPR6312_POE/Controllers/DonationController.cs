using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Controllers
{
    public class DonationController : Controller
    {
        // In-memory donation list (static for demo)
        private static List<Donation> _donations = new List<Donation>();

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
                _donations.Add(model);
                return RedirectToAction("ThankYou");
            }

            return View(model);
        }

        // GET: /Donation/List
        public IActionResult List()
        {
            return View(_donations);
        }

        // GET: /Donation/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
