using afi.university.ui.Models;
using afi.university.ui.Services.Interfaces;
using afi.university.ui.Services.Interfaces.Authentication;

namespace afi.university.ui.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IHttpService _httpService;

        public CourseService(IHttpService httpService)
        {
            this._httpService = httpService;
        }
        public async Task<IEnumerable<StudentCourses>> GetAllUniversityCourses()
        {
            return await _httpService.Get<IEnumerable<StudentCourses>>("/Course/GetAllUniversityCourses");
        }
    }
}
