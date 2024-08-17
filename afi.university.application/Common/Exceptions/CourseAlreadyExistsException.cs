namespace afi.university.application.Common.Exceptions
{
    public class CourseAlreadyExistsException: Exception
    {
        public CourseAlreadyExistsException(string courseName) : base($"Course  ({courseName}) already exist.") { }
    }
}
