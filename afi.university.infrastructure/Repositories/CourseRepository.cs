using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    internal class CourseRepository : BaseRepository<Course, ApplicationDbContext>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<Course> GetByIdAsync(int courseId, bool trackChanges)
        {
            var course = await GetByConditionAsync(c => c.Id.Equals(courseId), trackChanges);
            return course!.SingleOrDefault(); //TODO: Deal with this
        }

        public async Task<Course> GetCourseByNameAsync(string courseName, bool trackChanges)
        {
            var course = await GetByConditionAsync(c => c.Name!.Equals(courseName), trackChanges);            
            return course?.SingleOrDefault();
        }
    }
}
