using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(4, ErrorMessage = "Password should have minimum of 4 characters")]
        public string? Password { get; set; }
    }
}
