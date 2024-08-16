using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    internal class StudentCourseRepository : BaseRepository<StudentCourse, ApplicationDbContext>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<StudentCourse> GetByIdAsync(int studentCourseId, bool trackChanges)
        {
            var studentCourse = await GetByConditionAsync(c => c.Id.Equals(studentCourseId), trackChanges);
            return studentCourse!.SingleOrDefault(); //TODO: Deal with this
        }

        public async Task<IEnumerable<StudentCourse>> GetCoursesByStudentIdAsync(int studentId, bool trackChanges)
        {
            return await GetByConditionAsync(c => c.StudentId.Equals(studentId), trackChanges);        
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentsByCourseIdAsync(int courseId, bool trackChanges)
        {
            return await GetByConditionAsync(c => c.CourseId.Equals(courseId), trackChanges);
        }
    }
}
