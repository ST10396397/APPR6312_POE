using Microsoft.AspNetCore.Mvc;

namespace APPR6312_POE.Models
{
    public class VolunteerTaskAssignment
    {
        public int VolunteerId { get; set; }
        public int TaskId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;

        // Navigation props (optional)
        public Volunteer Volunteer { get; set; }
        public VolunteerTask Task { get; set; }
    }
}
