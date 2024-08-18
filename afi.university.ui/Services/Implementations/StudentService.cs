using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using afi.university.ui.Services.Interfaces;
using afi.university.ui.Services.Interfaces.HttpService;

namespace afi.university.ui.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IHttpService _httpService;

        public StudentService(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        /// <summary>
        /// Gets student details including courses
        /// </summary>
        /// <param name="studentRequest"></param>
        /// <returns></returns>
        public async Task<StudentResponse> GetStudentInformationAsync(StudentRequest studentRequest)
        {
            var response = await _httpService.Get<StudentResponse>($"/students/{studentRequest.StudentId}");
            return response;
        }

        /// <summary>
        /// Registers a student
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<RegistrationResponse> RegisterStudentAsync(RegistrationRequest registrationRequest)
        {
            var response = await _httpService.Post<RegistrationResponse>("/students/register", registrationRequest);
            return response;
        }

        /// <summary>
        /// Enrolls to a specific course
        /// </summary>
        /// <param name="courseRegistration"></param>
        /// <returns></returns>
        public async Task<bool> EnrollCourseAsync(CourseRegistrationRequest courseRegistration)
        {
            var response = await _httpService.Post<bool>("/students/enroll/", courseRegistration);
            return response;
        }

        public async Task<bool> DeRegisterCourseAsync(CourseRegistrationRequest courseRegistration)
        {
            var response = await _httpService.Post<bool>("/students/unenroll/", courseRegistration);
            return response;
        }

        //public async Task<IEnumerable<StudentCourses>> GetAllAvailableCourses()
        //{
        //    return await _httpService.Get<IEnumerable<StudentCourses>>("/Student/GetAllAvailableCourses");
        //}

        //public async Task<IEnumerable<StudentCourses>> GetRegisteredCoursesAsync(StudentCoursesRequest studentCoursesRequest)
        //{
        //    return await _httpService.Post<IEnumerable<StudentCourses>>("/Student/GetStudentRegisteredCourses", studentCoursesRequest);
        //}

        //public async Task<bool> RegisterToACourseAsync(CourseRegistrationRequest courseRegistration)
        //{            
        //    return await _httpService.Post<bool>("/Student/RegisterCourse", courseRegistration);
        //}

        //public async Task<bool> DeRegisterToACourseAsync(CourseRegistrationRequest courseRegistration)
        //{
        //    return await _httpService.Post<bool>("/Student/UnregisterFromACourse", courseRegistration);
        //}

        ///// <summary>
        ///// Register to University
        ///// </summary>
        ///// <param name="registrationRequest"></param>
        ///// <returns></returns>
        //public async Task<StudentRegistrationResponse> RegisterToUniversity(StudentRegistrationRequest registrationRequest)
        //{
        //    var response = await _httpService.Post<StudentRegistrationResponse>("/Student/RegisterToUniversity", registrationRequest);
        //    return response;
        //}
    }
}
