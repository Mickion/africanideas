using System.ComponentModel.DataAnnotations;

namespace afi.university.ui.Models
{
    public class StudentRegistrationRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public Roles? Role { get; set; }
    }
}
