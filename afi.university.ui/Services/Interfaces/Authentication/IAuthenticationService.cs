using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.ui.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        LoginResponse User { get; }
        Task InitializeAsync();
        Task LoginAsync(string email, string password);
        Task LogoutAysnc();
    }
}
