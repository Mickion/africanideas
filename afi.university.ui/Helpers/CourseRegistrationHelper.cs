using afi.university.ui.Services.Interfaces;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.ui.Helpers
{
    public class CourseRegistrationHelper
    {
        public static async Task<bool> EnrollOrUnregisterCourseAsync(IStudentService studentService, Guid studentId, CourseResponse course)
        {
            bool response;
            course!.Registered = !course.Registered;

            CourseRegistrationRequest courseRegistration = new()
            {
                StudentId = studentId,
                CourseId = course.Id
            };

            if (course!.Registered)            
                response = await studentService.EnrollCourseAsync(courseRegistration);            
            else            
                response = await studentService.DeRegisterCourseAsync(courseRegistration);
           
            return response;
        }
    }
}
