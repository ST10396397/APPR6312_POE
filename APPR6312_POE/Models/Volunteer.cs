using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class Volunteer : Controller
    {
        public int Id { get; set; }  // Unique volunteer ID

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}

