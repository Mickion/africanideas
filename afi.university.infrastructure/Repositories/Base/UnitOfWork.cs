using afi.university.domain.Repositories;
using afi.university.domain.Repositories.Base;
using afi.university.infrastructure.Persistence;

namespace afi.university.infrastructure.Repositories.Base
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<ILectureRepository> _lectureRepository;
        private readonly Lazy<IStudentRepository> _studentRepository;
        private readonly Lazy<IStudentCourseRepository> _studentCourseRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._userRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            this._courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(dbContext)); 
            this._lectureRepository = new Lazy<ILectureRepository>(() => new LectureRepository(dbContext));
            this._studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(dbContext));
            this._studentCourseRepository = new Lazy<IStudentCourseRepository>(() => new StudentCourseRepository(dbContext));
        }
        public IUserRepository Users => _userRepository.Value;

        public IStudentRepository Students => _studentRepository.Value;

        public ICourseRepository Courses => _courseRepository.Value;

        public ILectureRepository Lectures => _lectureRepository.Value;

        public IStudentCourseRepository StudentsCourse => _studentCourseRepository.Value;

        public void SaveChangesAsync()
        {
            //All the changes will be applied or if something fails, all the changes will be reverted:
            _dbContext.SaveChangesAsync();
        }
    }
}
