using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using afi.university.ui.Services.Interfaces;
using afi.university.ui.Services.Interfaces.HttpService;

namespace afi.university.ui.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IHttpService _httpService;

        public StudentService(IHttpService httpService) => this._httpService = httpService;
        
        /// <summary>
        /// Gets student details including courses
        /// </summary>
        /// <param name="studentRequest"></param>
        /// <returns></returns>
        public async Task<StudentResponse> GetStudentInformationAsync(StudentRequest studentRequest)
        {
            return await _httpService.Get<StudentResponse>($"/students/{studentRequest.StudentId}");            
        }

        /// <summary>
        /// Registers a student
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<RegistrationResponse> RegisterStudentAsync(RegistrationRequest registrationRequest)
        {
            return await _httpService.Post<RegistrationResponse>("/students/register", registrationRequest);            
        }

        /// <summary>
        /// Enrolls to a specific course
        /// </summary>
        /// <param name="courseRegistration"></param>
        /// <returns></returns>
        public async Task<bool> EnrollCourseAsync(CourseRegistrationRequest courseRegistration)
        {
            return await _httpService.Post<bool>("/students/enroll/", courseRegistration);            
        }

        /// <summary>
        /// Un-enroll a student from a specific course
        /// </summary>
        /// <param name="courseRegistration"></param>
        /// <returns></returns>
        public async Task<bool> DeRegisterCourseAsync(CourseRegistrationRequest courseRegistration)
        {
            return await _httpService.Post<bool>("/students/unenroll/", courseRegistration);            
        }

        /// <summary>
        /// Gets all students
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StudentResponse>> GetAllStudentsAsync() => await _httpService.Get<IEnumerable<StudentResponse>>("/students");
    }
}
