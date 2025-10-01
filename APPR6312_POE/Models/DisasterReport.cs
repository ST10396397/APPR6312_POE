using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class DisasterReport
    {
        [Required]
        [Display(Name = "Incident Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Incident")]
        public DateTime IncidentDate { get; set; }

        [Phone]
        [Display(Name = "Contact Phone (Optional)")]
        public string ContactPhone { get; set; }
    }
}
