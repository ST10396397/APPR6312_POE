using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Controllers
{
    public class DisasterController : Controller
    {
        public IActionResult Index()
        {
            return View("Disaster");
        }

        // POST: /Disaster/Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Report(DisasterReport model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save the report to database or send an email, etc.
                // For now, just display a Thank You page.

                return RedirectToAction("ThankYou");
            }

            // If validation failed, redisplay the form with errors
            return View(model);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
