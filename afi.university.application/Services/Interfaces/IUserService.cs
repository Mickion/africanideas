namespace afi.university.application.Services.Interfaces
{
    public interface IUserService
    {
        Task<shared.DataTransferObjects.Responses.LoginResponse> LoginAsync(shared.DataTransferObjects.Requests.LoginRequest loginRequest);
    }
}
