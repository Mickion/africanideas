using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;

namespace afi.university.application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<bool> AddCourseAsync(CourseRequestDto courseRequest);

        Task<List<StudentCoursesDto>> GetAllCoursesAsync();
    }
}
