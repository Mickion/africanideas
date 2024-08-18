using System.ComponentModel.DataAnnotations;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public class CreateCourseRequest
    {
        [Required(ErrorMessage = "Course name is a required field.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "NQF Level is a required field.")]
        [Range(5, 10, ErrorMessage = "NQF Level should range between 5 and 10")]
        public int NQFLevel { get; set; }

        [Required(ErrorMessage = "Course duration is a required field.")]
        public int Duration { get; set; } = 3; //Default 3 years
    }
}
