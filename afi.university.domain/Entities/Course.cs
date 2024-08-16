using afi.university.domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace afi.university.domain.Entities
{
    public class Course: BaseEntity
    {
        /// <summary>
        /// Gets or sets course name
        /// </summary>
        [Required(ErrorMessage = "Course name is a required field.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "NQF Level is a required field.")]
        [Range(5, 10, ErrorMessage ="NQF Level should range between 5 and 10")]
        public int NQFLevel  { get; set; }

        /// <summary>
        /// Gets or sets duration. No of years
        /// </summary>
        [Required(ErrorMessage = "Course duration is a required field.")]
        public int Duration { get; set; } = 3; //Default 3 years

        public ICollection<Student>? Students { get; set; }

    }
}
