using afi.university.ui.Models;

namespace afi.university.ui.Services.Interfaces
{
    public interface IStudentService
    {        
        Task<IEnumerable<StudentCourses>> GetRegisteredCoursesAsync(StudentCoursesRequest studentCoursesRequest);

        Task<IEnumerable<StudentCourses>> GetAllAvailableCourses();

        Task<StudentRegistrationResponse> RegisterToUniversity(StudentRegistrationRequest registrationRequest);

        Task<bool> RegisterToACourseAsync(CourseRegistrationRequest courseRegistration);
        Task<bool> DeRegisterToACourseAsync(CourseRegistrationRequest courseRegistration);
    }
}
