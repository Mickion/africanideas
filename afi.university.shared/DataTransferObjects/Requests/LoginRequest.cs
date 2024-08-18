using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public class LoginRequest
    {
        public LoginRequest(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(4, ErrorMessage = "Password should have minimum of 4 characters")]
        public string? Password { get; set; }
    }

}
