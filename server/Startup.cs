using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Sgi.Data;
using Radzen;
using Sgi.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Sgi.Handlers;

namespace Sgi
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            //services.AddScoped<HttpClient>(serviceProvider =>
            //{

            //  var uriHelper = serviceProvider.GetRequiredService<NavigationManager>();

            //  return new HttpClient
            //  {
            //    BaseAddress = new Uri(uriHelper.BaseUri)
            //  };
            //});

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            services.AddSingleton<HttpClient>();

            services.AddTransient<ValidateHeaderHandler>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddBlazoredLocalStorage();
            services.AddHttpClient<IUserService, UserService>();

            services.AddScoped<SgiCoreService>();
            services.AddScoped<GeocodingService>();


            services.AddDbContext<Sgi.Data.SgiCoreContext>(options =>
            {
              options.UseSqlServer(Configuration.GetConnectionString("SGICoreConnection"));
            });

            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(o =>
            {
                o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
            });

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();

            services.AddSingleton<HttpClient>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SeniorEmployee", policy =>
                    policy.RequireClaim("IsUserEmployedBefore1990", "true"));
            });

            OnConfigureServices(services);

        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            OnConfigure(app, env);
        }
    }


}
