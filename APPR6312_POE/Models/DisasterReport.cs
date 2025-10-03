using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class DisasterReport
    {
        [Key]  // Marks as Primary Key
        public int ReportId { get; set; }  // match DB column

        [Required]
        [Display(Name = "Incident Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description of Incident")]
        [DataType(DataType.MultilineText)]
        public string DisasterDescription { get; set; }

        [Required]
        [Display(Name = "Location of Incident")]
        public string DisasterLocation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Incident")]
        public DateTime IncidentDate { get; set; }

        [Phone]
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }
    }
}
