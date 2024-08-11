using afi.university.domain.Common.Enums;

namespace afi.university.application.Models.Requests
{
    public class StudentRegistrationRequestDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public UserRole? Role { get; set; }
    }
}
