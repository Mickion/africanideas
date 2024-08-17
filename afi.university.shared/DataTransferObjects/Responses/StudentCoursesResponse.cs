namespace afi.university.shared.DataTransferObjects.Responses
{
    public record StudentCoursesResponse (Guid Id, string Name, int NQFLevel, int duration)
    {
    }
}
