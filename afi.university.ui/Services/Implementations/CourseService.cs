
using afi.university.ui.Services.Interfaces;
using afi.university.shared.DataTransferObjects.Responses;
using afi.university.ui.Services.Interfaces.HttpService;
using afi.university.shared.DataTransferObjects.Requests;

namespace afi.university.ui.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IHttpService _httpService;
        public CourseService(IHttpService httpService) => _httpService = httpService;

        public async Task<bool> AddCourseAsync(CreateCourseRequest createCourseRequest)
        {
            var response = await _httpService.Post<bool>("/courses/", createCourseRequest);
            return response;
        }

        public async Task<IEnumerable<CourseResponse>> GetAllUniversityCoursesAsync()
        {
            var response =  await _httpService.Get<IEnumerable<CourseResponse>>("/courses");
            return response;
        }
    }
}
