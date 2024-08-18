using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.ui.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetAllUniversityCoursesAsync();
    }
}
