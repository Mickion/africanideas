using afi.university.domain.Common.Exceptions;
using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace afi.university.infrastructure.Repositories
{
    public class StudentCourseRepository : BaseRepository<StudentCourse, ApplicationDbContext>, IStudentCourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public override async Task<IEnumerable<StudentCourse>> GetAllAsync()
        {
            var courses = _dbContext.StudentCourses;

            await Task.CompletedTask;
            return courses ?? throw new NotFoundException("Courses not found.");
        }

        /// <summary>
        /// Get by student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public override async Task<StudentCourse> GetByIdAsync(int studentId)
        {
            var course = _dbContext.StudentCourses?
                .Where(cr => cr.StudentId == studentId)
                .FirstOrDefault();

            await Task.CompletedTask;
            return course ?? throw new NotFoundException($"Course id ({studentId}) not found.");

        }

        /// <summary>
        /// Gets by student id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<StudentCourse>> GetCoursesByStudentIdAsync(int studentId)
        {
            var studentCourses = _dbContext.StudentCourses?
                .Where(cr => cr.StudentId == studentId);                


            await Task.CompletedTask;
            return studentCourses ?? throw new NotFoundException($"Student id ({studentId}) courses not found.");            
        }

        /// <summary>
        /// Gets by course id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<StudentCourse>> GetStudentsByCourseIdAsync(int courseId)
        {
            var studentCourses = _dbContext.StudentCourses?
                .Where(cr => cr.StudentId == courseId);


            await Task.CompletedTask;
            return studentCourses ?? throw new NotFoundException($"Course id ({courseId}) students not found.");
        }
    }
}
