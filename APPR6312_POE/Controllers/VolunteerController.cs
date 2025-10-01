using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Controllers
{
    public class VolunteerController : Controller
    {
        // In-memory stores for demo
        private static List<Volunteer> _volunteers = new();
        private static List<VolunteerTask> _tasks = new()
        {
            new VolunteerTask { Id = 1, Name = "Distribute Food", Description = "Help distribute food packages", ScheduledDate = System.DateTime.Today.AddDays(1) },
            new VolunteerTask { Id = 2, Name = "Medical Aid Support", Description = "Assist medical staff", ScheduledDate = System.DateTime.Today.AddDays(2) },
            new VolunteerTask { Id = 3, Name = "Shelter Setup", Description = "Help set up shelters", ScheduledDate = System.DateTime.Today.AddDays(3) }
        };
        private static List<VolunteerTaskAssignment> _assignments = new();

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
                model.Id = _volunteers.Count + 1;
                _volunteers.Add(model);
                return RedirectToAction("ThankYou", new { volunteerId = model.Id });
            }
            return View(model);
        }

        // GET: /Volunteer/ThankYou/{volunteerId}
        public IActionResult ThankYou(int volunteerId)
        {
            var volunteer = _volunteers.FirstOrDefault(v => v.Id == volunteerId);
            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        // GET: /Volunteer/Tasks
        public IActionResult Tasks()
        {
            return View(_tasks);
        }

        // GET: /Volunteer/Assign/{volunteerId}/{taskId}
        public IActionResult Assign(int volunteerId, int taskId)
        {
            var volunteer = _volunteers.FirstOrDefault(v => v.Id == volunteerId);
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);

            if (volunteer == null || task == null || task.IsAssigned)
                return BadRequest("Invalid volunteer or task or task already assigned.");

            task.IsAssigned = true;
            _assignments.Add(new VolunteerTaskAssignment { VolunteerId = volunteerId, TaskId = taskId });

            return RedirectToAction("MyTasks", new { volunteerId });
        }

        // GET: /Volunteer/MyTasks/{volunteerId}
        public IActionResult MyTasks(int volunteerId)
        {
            var volunteer = _volunteers.FirstOrDefault(v => v.Id == volunteerId);
            if (volunteer == null) return NotFound();

            var assignedTasks = from a in _assignments
                                join t in _tasks on a.TaskId equals t.Id
                                where a.VolunteerId == volunteerId
                                select t;

            ViewBag.Volunteer = volunteer;
            return View(assignedTasks);
        }
    
        public IActionResult Index()
        {
            return View("Volunteer");
        }
    }
}
