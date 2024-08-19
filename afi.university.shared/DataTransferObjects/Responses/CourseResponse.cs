namespace afi.university.shared.DataTransferObjects.Responses
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int NQFLevel { get; set; }
        public bool Registered { get; set; }
        public int Duration { get; set; }        
    }
}
