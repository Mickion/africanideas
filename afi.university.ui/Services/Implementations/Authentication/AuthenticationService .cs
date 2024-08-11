using afi.university.ui.Models;
using afi.university.ui.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Components;

namespace afi.university.ui.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public User? User { get; private set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            //TODO: Is browser storage best practise?
            User = await _localStorageService.GetItem<User>("user");
        }

        public async Task Login(string email, string password)
        {
            User = await _httpService.Post<User>("/user/login", new { email, password });
            await _localStorageService.SetItem("user", User);
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
