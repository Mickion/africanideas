using afi.university.ui.Models;

namespace afi.university.ui.Services.Interfaces
{
    public interface IStudentService
    {        
        Task<IEnumerable<StudentCourses>> GetRegisteredCoursesAsync(int studentId);
    }
}
