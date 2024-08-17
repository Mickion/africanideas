namespace afi.university.application.Common.Exceptions
{
    public class CourseNotFoundException : NotFoundException
    {
        public CourseNotFoundException(Guid courseId) : base($"Course with Id ({courseId}) does not exist.") { }
    }
}
