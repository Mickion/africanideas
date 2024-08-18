using afi.university.domain.Common.Enums;

namespace afi.university.shared.DataTransferObjects.Responses
{
    public record LoginResponse(Guid Id, string? FirstName, string? LastName, string? Email, UserRole Role)
    {       
        public string? Token { get; set; }

        public LoginResponse() : this(default, default, default, default, default)
        {

        }
    }
}
