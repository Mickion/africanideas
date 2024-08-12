namespace afi.university.application.Models.Responses
{
    public class StudentCoursesDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Duration { get; set; }

        public bool Registered { get; set; }
    }
}
