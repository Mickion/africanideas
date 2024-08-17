using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public record CourseRegistrationRequest
    {
        [Required(ErrorMessage = "StudentId is a required field.")]
        public Guid StudentId { get; init; }

        [Required(ErrorMessage = "CourseId is a required field.")]
        public Guid CourseId { get; init; }
    }
}
