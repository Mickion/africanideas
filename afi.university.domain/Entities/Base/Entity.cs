using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace afi.university.domain.Entities.Base
{
    public class Entity
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [Key]                
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

    }
}
