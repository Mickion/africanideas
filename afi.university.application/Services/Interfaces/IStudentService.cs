using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;

namespace afi.university.application.Services.Interfaces
{
    public interface IStudentService
    {
        /// <summary>
        /// Enrolls student to the University
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        Task<StudentRegistrationResponseDto> RegisterToUniversityAsync(StudentRegistrationRequestDto registrationRequest);

        /// <summary>
        /// Unregister a specific course
        /// </summary>
        /// <returns></returns>
        Task<bool> UnregisterAsync(CourseRegistrationRequestDto courseRegistration);

        /// <summary>
        /// Enrolls a student to a particular course(s)
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        Task<bool> RegisterCourseAsync(CourseRegistrationRequestDto courseRegistration);

        /// <summary>
        /// Gets all registered course for student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<StudentCoursesDto>> GetRegisteredCoursesAsync(StudentCoursesRequestDto studentCoursesRequest);

        /// <summary>
        /// Gets all University students
        /// </summary>
        /// <returns></returns>
        Task<List<StudentsResponseDto>> GetAllUniversityStudentsAsync();
    }
}
