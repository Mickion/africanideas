using System.ComponentModel.DataAnnotations;

namespace afi.university.domain.Entities.Base
{
    public class Entity
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [Key]                        
        public Guid Id { get; set; }        

    }
}
