using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class VolunteerTask : Controller
    {
        public int Id { get; set; }  // Unique task ID

        [Required]
        [Display(Name = "Task Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; }

        public bool IsAssigned { get; set; } = false;
    }
}
