﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TBBlazorApp.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using TBBlazorApp.Interfaces;
using SGR.Models.Models;

namespace TBBlazorApp.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public ILoginService _userService { get; set; }        
        private readonly HttpClient _httpClient;        

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService,
            ILoginService userService,
            HttpClient httpClient)
        {
            //throw new Exception("CustomAuthenticationStateProviderException");
            _localStorageService = localStorageService;
            _userService = userService;
            _httpClient = httpClient;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {   
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");           
            
            ClaimsIdentity identity;

            if (accessToken != null && accessToken != string.Empty)
            {
                User user = await _userService.GetUserByAccessTokenAsync(accessToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }          

            var claimsPrincipal = new ClaimsPrincipal(identity);            

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(UserModel user)
        {
            await _localStorageService.SetItemAsync("accessToken", user.AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", user.RefreshToken);

            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("accessToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.EmailAddress != null)
            { 
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, user.EmailAddress),                                   
                                    new Claim(ClaimTypes.Role, user.IdRoleNavigation.Descripcion),
                                    new Claim("IsUserEmployedBefore1990", IsUserEmployedBefore1990(user))
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        private string IsUserEmployedBefore1990(User user)
        {
            return "false";
        }
    }
}
