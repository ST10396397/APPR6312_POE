using APPR6312_POE.Data;
using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public IActionResult Tasks(int volunteerId)
        {
            var tasks = _context.VolunteerTasks
        .Select(t => new TaskWithStatus
        {
            Task = t,
            IsAssigned = _context.VolunteerTaskAssignments.Any(a => a.TaskId == t.TaskId)
        })
        .ToList();

            ViewBag.VolunteerId = volunteerId; // Pass current user ID to view
            return View(tasks);
        }

        // GET: /Volunteer/Assign/{volunteerId}/{taskId}
        [HttpPost]
        public IActionResult Assign(int taskId, int volunteerId)
        {
            var assignment = new VolunteerTaskAssignment
            {
                TaskId = taskId,
                VolunteerId = volunteerId,
                AssignedDate = DateTime.Now
            };

            _context.VolunteerTaskAssignments.Add(assignment);
            _context.SaveChanges();

            return RedirectToAction("Tasks", new { volunteerId = volunteerId });
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

        [HttpPost]
        public IActionResult DeleteTask(int taskId, int volunteerId)
        {
            var task = _context.VolunteerTasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                _context.VolunteerTasks.Remove(task);
                _context.SaveChanges();
            }

            // Redirect back to Tasks with the volunteerId so the page reloads correctly
            return RedirectToAction("Tasks", new { volunteerId = volunteerId });
        }
    }
}
