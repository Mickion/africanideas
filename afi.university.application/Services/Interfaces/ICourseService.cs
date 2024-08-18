using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetAllCoursesAsync();

        Task<bool> AddCourseAsync(CreateCourseRequest createCourseRequest);

    }
}
