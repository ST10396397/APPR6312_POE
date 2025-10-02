using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;
using APPR6312_POE.Data;
using Microsoft.EntityFrameworkCore;

namespace APPR6312_POE.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly AppDbContext _context;

        public VolunteerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Volunteer/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Volunteer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Volunteer model)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(model);
                _context.SaveChanges();

                return RedirectToAction("ThankYou", new { volunteerId = model.VolunteerId });
            }
            return View(model);
        }

        // GET: /Volunteer/ThankYou/{volunteerId}
        public IActionResult ThankYou(int volunteerId)
        {
            var volunteer = _context.Volunteers.FirstOrDefault(v => v.VolunteerId == volunteerId);
            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        // GET: /Volunteer/Tasks
        public IActionResult Tasks()
        {
            var tasks = _context.VolunteerTasks.ToList();
            return View(tasks);
        }

        // GET: /Volunteer/Assign/{volunteerId}/{taskId}
        public IActionResult Assign(int volunteerId, int taskId)
        {
            var volunteer = _context.Volunteers.Find(volunteerId);
            var task = _context.VolunteerTasks.Find(taskId);

            if (volunteer == null || task == null || task.IsAssigned)
                return BadRequest("Invalid volunteer or task or task already assigned.");

            task.IsAssigned = true;
            _context.VolunteerTaskAssignments.Add(new VolunteerTaskAssignment
            {
                VolunteerId = volunteerId,
                TaskId = taskId
            });

            _context.SaveChanges();

            return RedirectToAction("MyTasks", new { volunteerId });
        }

        // GET: /Volunteer/MyTasks/{volunteerId}
        public IActionResult MyTasks(int volunteerId)
        {
            var volunteer = _context.Volunteers.Find(volunteerId);
            if (volunteer == null) return NotFound();

            var assignedTasks = _context.VolunteerTaskAssignments
                .Include(a => a.Task)
                .Where(a => a.VolunteerId == volunteerId)
                .Select(a => a.Task)
                .ToList();

            ViewBag.Volunteer = volunteer;
            return View(assignedTasks);
        }

        public IActionResult Index()
        {
            return View("Volunteer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(VolunteerTask model)
        {
            if (ModelState.IsValid)
            {
                _context.VolunteerTasks.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Tasks");
            }

            // If validation fails, reload the page with tasks
            var tasks = _context.VolunteerTasks.ToList();
            ViewBag.NewTask = model;
            return View("Tasks", tasks);
        }
    }
}
