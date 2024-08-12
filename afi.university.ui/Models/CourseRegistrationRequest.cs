using System.ComponentModel.DataAnnotations;

namespace afi.university.ui.Models
{
    public class CourseRegistrationRequest
    {
        [Required]
        public int StudentId { get; set; }

        public string? Name { get; set; }

        public int Duration { get; set; }
    }
}
