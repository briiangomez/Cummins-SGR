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
using Microsoft.EntityFrameworkCore;
using SGI.DAL.Contracts;
using SGI.DAL.Concrete;
using Microsoft.AspNetCore.Identity;
using SGI.DAL.DataAccess;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorAppAuth.Areas.Identity;
using SGI.Entities;
using Connector;

namespace SGI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //https://www.yogihosting.com/aspnet-core-identity-setup/
            services.AddDbContext<SecurityDBContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("SecurityConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<SecurityDBContext>();

            services.AddDbContext<ApplicationDBContext>(options =>
                          options.UseSqlServer(
                              Configuration.GetConnectionString("ApplicationConnection")));

            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ; //Habilita el modo Client-Server y errores detallados...
        
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            //Article and Category as service  
            services.AddScoped<IManager<Article>, ArticleManager>(); //Scoped/singleton/transient
            services.AddScoped<IManager<Category>, CategoryManager>();
            //Register dapper in scope  
            services.AddScoped<IDapperManager, DapperManager>();
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
                //RFC 6797 -> Strict Transport-Security" The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                endpoints.MapBlazorHub(); //Asocia Blazor Client-Server con SignalR desde el lado del servidor
                endpoints.MapHub<HubConnector>("/connector");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
