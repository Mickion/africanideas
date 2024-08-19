namespace afi.university.shared.DataTransferObjects.Responses
{
    public class CourseStudentsResponse: CourseResponse
    {
        public List<StudentResponse>? Students { get; set; }
    }
}
