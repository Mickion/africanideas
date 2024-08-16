namespace afi.university.application.Models.Responses
{
    public class StudentCoursesDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public int Duration { get; set; }

        public bool Registered { get; set; }
    }
}
