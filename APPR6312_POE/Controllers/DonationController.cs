using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Controllers
{
    public class DonationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
