using afi.university.ui.Models;

namespace afi.university.ui.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<StudentCourses>> GetAllUniversityCourses();
    }
}
