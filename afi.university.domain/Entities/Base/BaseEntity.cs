using System.ComponentModel.DataAnnotations;

namespace afi.university.domain.Entities.Base
{
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [Key]                        
        public Guid Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }

    }
}
