using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class User
    {
        [Key]  // Primary Key
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }  

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string HashedPassword { get; set; } 
    }
}

