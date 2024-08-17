using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public record CreateCourseRequest ()
    {
        [Required(ErrorMessage = "Course name is a required field.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "NQF Level is a required field.")]
        [Range(5, 10, ErrorMessage = "NQF Level should range between 5 and 10")]
        public int NQFLevel { get; init; }

        [Required(ErrorMessage = "Course duration is a required field.")]
        public int Duration { get; init; } = 3; //Default 3 years
    }
}
