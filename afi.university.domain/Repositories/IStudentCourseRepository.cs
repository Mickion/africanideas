using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;

namespace afi.university.domain.Repositories
{
    public interface IStudentCourseRepository: IRepositoryBase<StudentCourse>
    {
        Task<IEnumerable<StudentCourse>> GetCoursesByStudentIdAsync(int studentId, bool trackChanges);

        Task<IEnumerable<StudentCourse>> GetStudentsByCourseIdAsync(int courseId, bool trackChanges);
    }
}
