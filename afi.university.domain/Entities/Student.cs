using afi.university.domain.Entities.Base;

namespace afi.university.domain.Entities
{
    public class Student: User
    {
        /// <summary>
        /// Gets or sets student Number
        /// </summary>
        public string? StudentNumber { get; set; }

        public ICollection<Course>? Courses { get; set; }

    }
}
