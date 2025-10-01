using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Models
{
    public class VolunteerTaskAssignment : Controller
    {
        public int VolunteerId { get; set; }
        public int TaskId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
