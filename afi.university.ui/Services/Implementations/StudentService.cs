using afi.university.ui.Models;
using afi.university.ui.Services.Interfaces;
using afi.university.ui.Services.Interfaces.Authentication;

namespace afi.university.ui.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IHttpService _httpService;

        public StudentService(IHttpService httpService)
        {
            this._httpService = httpService;
        }
        public async Task<IEnumerable<StudentCourses>> GetRegisteredCoursesAsync(int studentId)
        {
            return await _httpService.Get<IEnumerable<StudentCourses>>("/Student/GetStudentCourses");
        }
    }
}
