namespace afi.university.shared.DataTransferObjects.Responses
{
    public record CourseResponse (Guid Id, string? Name, int NQFLevel, int Duration)
    {
        public bool Registered { get; set; }
        public CourseResponse() : this(default, default, default, default) { }
    }
}
