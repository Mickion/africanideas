using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;

namespace afi.university.domain.Repositories
{
    public interface IStudentCourseRepository: IBaseRepository<StudentCourse>
    {
        Task<IEnumerable<StudentCourse>> GetCoursesByStudentIdAsync(Guid studentId, bool trackChanges);

        Task<IEnumerable<StudentCourse>> GetStudentsByCourseIdAsync(Guid courseId, bool trackChanges);
    }
}
