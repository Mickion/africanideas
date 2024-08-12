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

    }
}
