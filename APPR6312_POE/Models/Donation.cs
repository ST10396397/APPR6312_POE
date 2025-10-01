using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class Donation
    {
        [Required]
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; }

        [Required]
        [Display(Name = "Resource Type")]
        public string ResourceType { get; set; }  // food, clothing, medical supplies, etc.

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Donation")]
        public DateTime DonationDate { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }
    }
}
