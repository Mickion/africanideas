using Microsoft.EntityFrameworkCore;
using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.domain.Common.Exceptions;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    public class StudentRepository: BaseRepository<Student, ApplicationDbContext>, IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
            var students = _dbContext.Students?
                    .Include(a => a.Courses);

            await Task.CompletedTask;
            return students ?? throw new NotFoundException("Students not found.");
        }

        public override async Task<Student> GetByIdAsync(int id)
        {
            var student = _dbContext.Students?
                .Where(cr => cr.Id.ToString() == id.ToString())
                .Include(a => a.Courses)
                .FirstOrDefault();

            await Task.CompletedTask;
            return student ?? throw new NotFoundException($"Student id ({id}) not found.");

        }
    }
}
