using afi.university.domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace afi.university.domain.Entities.Base
{
    public class User : Entity
    {

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        public string? FirstName { get; set; } 

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets email address
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        [Required]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets role. Admin or student
        /// </summary>
        [Required]
        public UserRole Role { get; set; }
    }
}
