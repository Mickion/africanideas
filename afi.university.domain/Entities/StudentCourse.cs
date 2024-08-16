using afi.university.domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace afi.university.domain.Entities
{
    public class StudentCourse: Entity
    {
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Student? Student { get; set;}

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course? Course { get; set;}
    }
}
