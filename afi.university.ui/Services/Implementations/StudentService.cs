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

        public async Task<IEnumerable<StudentCourses>> GetAllAvailableCourses()
        {
            return await _httpService.Get<IEnumerable<StudentCourses>>("/Student/GetAllAvailableCourses");
        }

        public async Task<IEnumerable<StudentCourses>> GetRegisteredCoursesAsync(StudentCoursesRequest studentCoursesRequest)
        {
            return await _httpService.Post<IEnumerable<StudentCourses>>("/Student/GetStudentRegisteredCourses", studentCoursesRequest);
        }

        public async Task<bool> RegisterToACourseAsync(CourseRegistrationRequest courseRegistration)
        {            
            return await _httpService.Post<bool>("/Student/RegisterCourse", courseRegistration);
        }
     
        public async Task<bool> DeRegisterToACourseAsync(CourseRegistrationRequest courseRegistration)
        {
            return await _httpService.Post<bool>("/Student/UnregisterFromACourse", courseRegistration);
        }

        /// <summary>
        /// Register to University
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<StudentRegistrationResponse> RegisterToUniversity(StudentRegistrationRequest registrationRequest)
        {
            var response = await _httpService.Post<StudentRegistrationResponse>("/Student/RegisterToUniversity", registrationRequest);
            return response;
        }
    }
}
