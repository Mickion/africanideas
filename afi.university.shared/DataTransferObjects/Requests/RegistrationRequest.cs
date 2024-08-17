using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public record RegistrationRequest
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        public string? FirstName { get; init; }

        [Required(ErrorMessage = "LastName is a required field.")]
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(4, ErrorMessage = "Password should have minimum of 4 characters")]
        public string? Password { get; init; }
    }
}
