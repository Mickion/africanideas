using afi.university.domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace afi.university.domain.Entities.Base
{
    /// <summary>
    /// Entity for loggin in to the system
    /// </summary>
    public class User : Entity
    {

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        [Required(ErrorMessage = "First name is a required field.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        [Required(ErrorMessage = "Last name is a required field.")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets email address
        /// </summary>                
        [Required(ErrorMessage = "Email address is a required field.")]        
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>        
        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(4, ErrorMessage ="Password should have minimum of 4 characters")]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets role. Admin or student
        /// </summary>
        [Required(ErrorMessage = "User role is a required field.")]
        public UserRole Role { get; set; }
    }
}
