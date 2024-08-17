namespace afi.university.application.Common.Exceptions
{
    public class StudentNotFoundException: NotFoundException
    {
        public StudentNotFoundException(Guid studentId) : base($"Student with Id ({studentId}) does not exist.") { }
    }
}
