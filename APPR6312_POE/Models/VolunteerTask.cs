using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class VolunteerTask
    {
        [Key]  // Primary Key
        public int TaskId { get; set; }  // match DB column

        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Description")]
        public string TaskDescription { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; }

        public bool IsAssigned { get; set; } = false;
    }
}
