using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.ui.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentResponse> GetStudentInformationAsync(StudentRequest studentRequest);

        Task<RegistrationResponse> RegisterStudentAsync(RegistrationRequest registrationRequest);

        Task<bool> EnrollCourseAsync(CourseRegistrationRequest courseRegistration);

        Task<bool> DeRegisterCourseAsync(CourseRegistrationRequest courseRegistration);

        Task<IEnumerable<StudentResponse>> GetAllStudentsAsync();
    }
}
