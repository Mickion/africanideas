﻿using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using afi.university.ui.Services.Interfaces.Authentication;
using afi.university.shared.DataTransferObjects.Responses;
using afi.university.ui.Services.Interfaces.HttpService;

namespace afi.university.ui.Services.Implementations.HttpService
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
            var user = await _localStorageService.GetItem<LoginResponse>("user");

            //var isApiUrl = !request.RequestUri!.IsAbsoluteUri;

            if (user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var error = await response.Content.ReadAsStringAsync();
                //_navigationManager.NavigateTo("logout");
                throw new Exception(error);                
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            var results = await response.Content.ReadFromJsonAsync<T>();


            return results!;
        }

        #endregion
    }
}
