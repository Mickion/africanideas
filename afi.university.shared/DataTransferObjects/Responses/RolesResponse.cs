using afi.university.domain.Common.Enums;

namespace afi.university.shared.DataTransferObjects.Responses
{
    public static class RolesResponse
    {
        public static UserRole Admin { get; set; } = UserRole.Admin;
        public static UserRole Student { get; set; } = UserRole.Student;
        public static UserRole Lecture { get; set; } = UserRole.Lecture;
    }
}
