using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGRBlazorApp.Data;
using EmbeddedBlazorContent;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using SGRBlazorApp.Services;
using SGRBlazorApp.Handlers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using SGRBlazorApp.Interfaces;
using Radzen;
using SGRBlazorApp.Models;
using SGR.Models.Models;

namespace SGRBlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<IFileUpload, FileUpload>();
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            
            var smtpSettingSection = Configuration.GetSection("SmtpSettings");
            services.Configure<SmtpSettings>(smtpSettingSection);
            services.AddSingleton<IEmailSenderService, EmailSenderService>();
            services.AddTransient<ValidateHeaderHandler>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            
            services.AddBlazoredLocalStorage();
            services.AddHttpClient<IUserService, UserService>();

            services.AddHttpClient<ISgrService<IncidenciaApi>, SgrService<IncidenciaApi>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Sintoma>, SgrService<Sintoma>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<MotorDealer>, SgrService<MotorDealer>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Motor>, SgrService<Motor>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Equipo>, SgrService<Equipo>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Incidencia>, SgrService<Incidencia>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Dealer>, SgrService<Dealer>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Certificacion>, SgrService<Certificacion>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<CertificacionMotor>, SgrService<CertificacionMotor>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<CertificacionDealer>, SgrService<CertificacionDealer>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Cliente>, SgrService<Cliente>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Sintoma>, SgrService<Sintoma>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Role>, SgrService<Role>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<SGRBlazorApp.Data.User>, SgrService<SGRBlazorApp.Data.User>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            //services.AddHttpClient<ISgrService<SGR.Models.Models.User>, SgrService<SGR.Models.Models.User>>()
            //        .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<EstadoIncidencium>, SgrService<EstadoIncidencium>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Estado>, SgrService<Estado>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Survey>, SgrService<Survey>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<SurveyItem>, SgrService<SurveyItem>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<SurveyItemOption>, SgrService<SurveyItemOption>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<SurveyAnswer>, SgrService<SurveyAnswer>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<IncidenciaSurvey>, SgrService<IncidenciaSurvey>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<DTOSurvey>, SgrService<DTOSurvey>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Setting>, SgrService<Setting>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<ImagenesIncidencium>, SgrService<ImagenesIncidencium>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Oem>, SgrService<Oem>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Pai>, SgrService<Pai>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ISgrService<Provincium>, SgrService<Provincium>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddSingleton<HttpClient>();
            services.AddScoped<GeocodingService>();
            services.AddScoped<DialogService>();
            services.AddScoped<TooltipService>();
            services.AddAuthorization(options => 
            {
                options.AddPolicy("SeniorEmployee", policy => 
                    policy.RequireClaim("IsUserEmployedBefore1990","true"));
            });

            services.AddHttpClient<ProtectedApiCallHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");                
            });
        }
    }
}
