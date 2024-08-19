using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.ui.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetAllUniversityCoursesAsync();

        Task<bool> AddCourseAsync(CreateCourseRequest createCourseRequest);

        Task<CourseStudentsResponse> GetCourseStudentsAsync(Guid courseId);
    }
}
