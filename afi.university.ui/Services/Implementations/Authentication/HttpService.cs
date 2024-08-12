using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using afi.university.ui.Models;
using afi.university.ui.Services.Interfaces.Authentication;

namespace afi.university.ui.Services.Implementations.Authentication
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private IConfiguration _configuration;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<T> Get<T>(string uri)
        {
            var apiUrl = new Uri(_configuration["apiUrl"]);
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl + uri);
            return await SendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var apiUrl = new Uri(_configuration["apiUrl"]);
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl + uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }

        #region Private implementations

        /// <summary>
        /// Adds jwt header to api calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var user = await _localStorageService.GetItem<User>("user");

            //var isApiUrl = !request.RequestUri!.IsAbsoluteUri;

            if (user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            
            return await response.Content.ReadFromJsonAsync<T>();
        }

        #endregion
    }
}
