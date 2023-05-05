using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SGRBlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStoreWebAPI;
using SGRBlazorApp.Interfaces;
using SGR.Models;

namespace SGRBlazorApp.Services
{
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }

        public UserService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            httpClient.BaseAddress = new Uri(_appSettings.BookStoresBaseAddress);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "SgiBlazorApp");

            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(User user)
        {
            try
            {
                user.Password = Utility.Encrypt(user.Password);
                string serializedUser = JsonConvert.SerializeObject(user);

                Logger.AddLine(String.Format("Data user receeived: {0}-{1}-{2}", DateTime.Now.ToString(), user.EmailAddress, user.Password));
                Logger.AddLine(String.Format("User Serialized: {0}-{1}", DateTime.Now.ToString(), serializedUser));

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
                requestMessage.Content = new StringContent(serializedUser);

                requestMessage.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                string serializeRequest = JsonConvert.SerializeObject(requestMessage);

                Logger.AddLine(String.Format("Request Enviado: {0}", serializeRequest));

                var response = await _httpClient.SendAsync(requestMessage);

                string serializeMssj = JsonConvert.SerializeObject(response);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                Logger.AddLine(String.Format("Info HttpClient Completa {0}-{1}-{2}-{3}", DateTime.Now.ToString(), responseBody, responseStatusCode.ToString(), serializeMssj));

                var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

                return await Task.FromResult(returnedUser);
            }
            catch (Exception ex)
            {
                Logger.AddLine(String.Format("Error desde UserService.cs - {0}-{1}-{2}", DateTime.Now.ToString(), ex.Message, ex.StackTrace));
                return await Task.FromResult(new User());
            }

        }

        public async Task<User> RegisterUserAsync(User user)
        {
            user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RegisterUser");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<User> RefreshTokenAsync(RefreshRequest refreshRequest)
        {
            string serializedUser = JsonConvert.SerializeObject(refreshRequest);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RefreshToken");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
            requestMessage.Content = new StringContent(serializedRefreshRequest);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }
    }
}
