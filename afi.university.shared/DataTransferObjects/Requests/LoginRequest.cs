using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public record LoginRequest
    {
        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(4, ErrorMessage = "Password should have minimum of 4 characters")]
        public string? Password { get; init; }
    }

}
