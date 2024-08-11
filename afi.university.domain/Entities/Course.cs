using afi.university.domain.Entities.Base;

namespace afi.university.domain.Entities
{
    public class Course: Entity
    {
        /// <summary>
        /// Gets or sets course name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets duration. No of years
        /// </summary>
        public int Duration { get; set; } = 3; //Default 3 years

        /// <summary>
        /// Gets or sets list of students registered to this course
        /// </summary>
        public List<Student>? Students { get; set; }
    }
}
