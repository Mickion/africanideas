using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
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
        Task<StudentResponse> GetStudentCoursesAsync(StudentRequest studentRequest);

        /// <summary>
        /// Gets all University students
        /// </summary>
        /// <returns></returns>
        Task<List<StudentsResponseDto>> GetAllUniversityStudentsAsync();
    }
}
