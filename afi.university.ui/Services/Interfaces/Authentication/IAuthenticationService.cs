using afi.university.ui.Models;

namespace afi.university.ui.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        User User { get; }
        Task Initialize();
        Task Login(string email, string password);
        Task Logout();
    }
}
