using Microsoft.EntityFrameworkCore;
using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.domain.Common.Exceptions;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course, ApplicationDbContext>, ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public override async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = _dbContext.Courses;                    

            await Task.CompletedTask;
            return courses ?? throw new NotFoundException("Courses not found.");
        }

        public override async Task<Course> GetByIdAsync(int id)
        {
            var course = _dbContext.Courses?
                .Where(cr => cr.Id == id)
                .FirstOrDefault();

            await Task.CompletedTask;
            return course ?? throw new NotFoundException($"Course id ({id}) not found.");

        }

        public async Task<Course> GetCourseByNameAsync(string courseName)
        {
            var course = _dbContext.Courses?
                .Where(cr => cr.Name == courseName)
                .FirstOrDefault();

            await Task.CompletedTask;
            return course ?? throw new NotFoundException($"Course name ({courseName}) not found.");
        }
    }
}
