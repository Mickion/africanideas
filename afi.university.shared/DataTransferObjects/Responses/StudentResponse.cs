using afi.university.domain.Common.Enums;

namespace afi.university.shared.DataTransferObjects.Responses
{
    public record StudentResponse(Guid Id, string? FirstName, string? LastName, string? Email, string? StudentNumber)
    {
        public ICollection<CourseResponse>? Courses { get; init; }
    }
}
