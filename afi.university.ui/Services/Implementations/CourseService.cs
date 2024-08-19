
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

        /// <summary>
        /// Creates a new course
        /// </summary>
        /// <param name="createCourseRequest"></param>
        /// <returns></returns>
        public async Task<bool> AddCourseAsync(CreateCourseRequest createCourseRequest)
        {
            return await _httpService.Post<bool>("/courses/", createCourseRequest);            
        }

        /// <summary>
        /// Gets all available course
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseResponse>> GetAllUniversityCoursesAsync()
        {
            return await _httpService.Get<IEnumerable<CourseResponse>>("/courses");            
        }

        /// <summary>
        /// Gets students registered per course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<CourseStudentsResponse> GetCourseStudentsAsync(Guid courseId)
        {
            var response = await _httpService.Get<CourseStudentsResponse>($"/courses/{courseId}/students");
            return response;
        }
    }
}
