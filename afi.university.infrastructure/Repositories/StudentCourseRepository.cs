using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    internal class StudentCourseRepository : BaseRepository<StudentCourse, ApplicationDbContext>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<StudentCourse> GetByIdAsync(Guid studentCourseId, bool trackChanges)
        {
            var studentCourses = await GetByConditionAsync(c => c.Id.Equals(studentCourseId), trackChanges);
            return studentCourses.SingleOrDefault();
        }

        public async Task<IEnumerable<StudentCourse>> GetCoursesByStudentIdAsync(Guid studentId, bool trackChanges)
        {
            return await GetByConditionAsync(c => c.StudentId.Equals(studentId), trackChanges);        
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentsByCourseIdAsync(Guid courseId, bool trackChanges)
        {
            return await GetByConditionAsync(c => c.CourseId.Equals(courseId), trackChanges);
        }
    }
}
