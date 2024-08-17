using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.application.Services.Interfaces
{
    public interface IStudentService
    {
        /// <summary>
        /// Gets all registered students
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StudentResponse>> GetAllStudentsAsync();

        /// <summary>
        /// Gets all registered course for student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentResponse> GetStudentRegisteredCoursesAsync(StudentRequest studentRequest);

        /// <summary>
        /// Enrolls student to the University
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        Task<RegistrationResponse> RegisterStudentAsync(RegistrationRequest registrationRequest);

        /// <summary>
        /// Unregister a specific course
        /// </summary>
        /// <returns></returns>
        Task<bool> UnregisterAsync(CourseRegistrationRequest courseRegistrationRequest);

        /// <summary>
        /// Enrolls a student to a particular course(s)
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        Task<bool> EnrollCourseAsync(CourseRegistrationRequest courseRegistrationRequest);



    }
}
