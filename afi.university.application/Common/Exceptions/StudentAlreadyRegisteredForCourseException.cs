namespace afi.university.application.Common.Exceptions
{
    public class StudentAlreadyRegisteredForCourseException: Exception
    {
        public StudentAlreadyRegisteredForCourseException(Guid studentId, Guid courseId) 
            : base($"Student ID ({studentId}) already registered for course ID ({courseId}).")
        {
            
        }
    }
}
