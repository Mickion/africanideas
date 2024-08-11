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
        /// Enrolls a student to a particular course(s)
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        Task<List<StudentCoursesDto>> RegisterToACourseAsync(int studentId, List<StudentCoursesDto> courseRegistrationRequest);

        /// <summary>
        /// Unregister a specific course
        /// </summary>
        /// <returns></returns>
        Task<List<StudentCoursesDto>> UnregisterAsync(int studentId, List<StudentCoursesDto> courseRegistrationRequest);

        /// <summary>
        /// Gets all registered course for student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<StudentCoursesDto>> GetRegisteredCoursesAsync(int studentId);

        /// <summary>
        /// Gets all University students
        /// </summary>
        /// <returns></returns>
        Task<List<StudentsResponseDto>> GetAllUniversityStudentsAsync();
    }
}
