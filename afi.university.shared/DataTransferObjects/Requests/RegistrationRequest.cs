namespace afi.university.shared.DataTransferObjects.Requests
{
    public record RegistrationRequest
    {
        public string? FirstName { get; init; }

        public int LastName { get; init; }

        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}
