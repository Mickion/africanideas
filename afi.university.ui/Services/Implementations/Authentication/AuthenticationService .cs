using Microsoft.AspNetCore.Components;
using afi.university.shared.DataTransferObjects.Responses;
using afi.university.ui.Services.Interfaces.Authentication;
using afi.university.shared.DataTransferObjects.Requests;
using System.IdentityModel.Tokens.Jwt;
using afi.university.ui.Services.Interfaces.HttpService;

namespace afi.university.ui.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {        
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public LoginResponse? User {
            get;
            private set;
        }

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

        public async Task InitializeAsync()
        {
            //TODO: Is browser storage best practise?
            User = await _localStorageService.GetItem<LoginResponse>("user");
            if(User != null)
            {
                var jwtToken = new JwtSecurityToken(User.Token);
                if (DateTime.UtcNow > jwtToken.ValidTo) await LogoutAysnc();                
            }

        }

        public async Task LoginAsync(string email, string password)
        {                       
            LoginRequest loginRequest = new(email, password);
            User = await _httpService.Post<LoginResponse>("/User/Login", loginRequest);

            if(User != null)
                await _localStorageService.SetItem("user", User);
        }

        public async Task LogoutAysnc()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
