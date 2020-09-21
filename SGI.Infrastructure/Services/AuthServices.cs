using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SGI.ApplicationCore.Exceptions;
using SGI.Infrastructure.Entities;
using SGI.Infrastructure.Interfaces;
using SGI.Infrastructure.Options;
using Newtonsoft.Json;

namespace SGI.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private HttpClient httpClient;
        private JwtOption jwtOption;

        public AuthService(IOptions<JwtOption> option, HttpClient httpClient, ILogger<AuthService> logger)
        {
            this.httpClient = httpClient;
            this.jwtOption = option.Value;
        }

        public async Task<Token> LoginAsync(string userName, string password)
        {
            Token token = default(Token);
            var values = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                //new KeyValuePair<string, string>("resource", jwtOption.Resource),
                //new KeyValuePair<string, string>("scope", jwtOption.Scope)
                //new KeyValuePair<string, string>("client_id", _option.ClientId),
                //new KeyValuePair<string, string>("client_secret", _option.ClientSecret)
            };

            using (HttpContent content = new FormUrlEncodedContent(values))
            {
                HttpResponseMessage response = await httpClient.PostAsync(jwtOption.AuthEndpoint, content);
                var responseFromServer = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    token = JsonConvert.DeserializeObject<Token>(responseFromServer);
                }
                else
                {
                    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responseFromServer);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new BusinessException(400, "ERROR_INVALID_CREDENTIALS");
                    }
                    throw new BusinessException(400, error.Description);
                }
            }

            return token;
        }


        public async Task<Token> RefreshTokenAsync(string refreshToken)
        {
            Token token = default(Token);
            var values = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            };

            using (HttpContent content = new FormUrlEncodedContent(values))
            {
                HttpResponseMessage response = await httpClient.PostAsync(jwtOption.AuthEndpoint, content);
                var responseFromServer = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    token = JsonConvert.DeserializeObject<Token>(responseFromServer);
                }
                else
                {
                    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responseFromServer);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new BusinessException(400, "CANT_REFRESH_TOKEN");
                    }
                    throw new BusinessException(400, error.Description);
                }
            }

            return token;
        }
    }
}
