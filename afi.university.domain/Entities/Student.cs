using afi.university.domain.Entities.Base;

namespace afi.university.domain.Entities
{
    public class Student: User
    {
        /// <summary>
        /// Gets or sets student Number
        /// </summary>
        public string? StudentNumber { get; set; }


        /// <summary>
        /// Gets or sets a list of courses this student is enrolled in
        /// </summary>
        public List<Course>? Courses { get; set; }
    }
}
