﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SGRBlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using SGRBlazorApp.Interfaces;
using SGR.Models;

namespace SGRBlazorApp.Services
{
    public class SgrService<T> : ISgrService<T>
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public ILocalStorageService _localStorageService { get; }

        public SgrService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;

            httpClient.BaseAddress = new Uri(_appSettings.BookStoresBaseAddress);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "SgiBlazorApp");

            _httpClient = httpClient;
        }

        public async Task<bool> DeleteAsync(string requestUri, Guid Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            return await Task.FromResult(true);
        }

        public async Task<List<T>> GetAllAsync(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(responseBody));
            }
            else
                return null;
        }

        public async Task<T> GetByIdAsync(string requestUri, Guid Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
        }

        public async Task<T> SaveAsync(string requestUri, T obj)
        {
            string serializedUser = JsonConvert.SerializeObject(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            Logger.AddLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), response.StatusCode,responseBody));

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }

        public async Task<T> UpdateAsync(string requestUri, Guid Id, T obj)
        {
            string serializedUser = JsonConvert.SerializeObject(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri + Id);
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            Logger.AddLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), response.StatusCode, responseBody));

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }
    }
}
