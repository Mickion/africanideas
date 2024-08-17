namespace afi.university.application.Common.Exceptions
{
    public class StudentAlreadyExistsException: Exception
    {
        public StudentAlreadyExistsException(string email): base($"Student email ({email}) already in use.")
        {            
        }
    }
}
