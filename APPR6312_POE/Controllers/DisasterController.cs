using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;
using APPR6312_POE.Data;

namespace APPR6312_POE.Controllers
{
    public class DisasterController : Controller
    {
        private readonly AppDbContext _context;

        public DisasterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Disaster/
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
                _context.DisasterReports.Add(model);
                _context.SaveChanges();

                return RedirectToAction("ThankYou");
            }

            // If validation failed, redisplay form
            return View("Disaster", model);
        }

        // GET: /Disaster/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }

        // GET: /Disaster/CurrentDisaster
        public IActionResult CurrentDisaster()
        {
            var disasters = _context.DisasterReports
                .OrderByDescending(d => d.IncidentDate)
                .ToList();

            return View(disasters); // you’ll need a list view
        }
    }
}
