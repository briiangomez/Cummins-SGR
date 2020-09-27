using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SGI.Infrastructure.Options;
using AutoMapper;
using SGI.Infrastructure.UnitOfWorks;
using SGI.Infrastructure.Entities;
using SGI.Infrastructure.Repositories;
using SGI.ApplicationCore.Interfaces;
using SGI.ApplicationCore.Options;
using SGI.ApplicationCore.Services;
using SGI.Infrastructure;
using SGI.ApplicationCore.Entities;

namespace SGI.WebApp
{
    public class Startup
    {

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.Authority = Configuration["Jwt:Authority"];
                o.Audience = Configuration["Jwt:Audience"];
                o.IncludeErrorDetails = true;

                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = "user_realm_roles"
                };

                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        if (Environment.IsDevelopment())
                        {
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }
                        return c.Response.WriteAsync("An error occured processing your authentication.");
                    }

                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SGI-WebApp Api",
                    Version = "v1"
                });

                c.CustomSchemaIds(x => x.FullName);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

                // Swagger 2.+ support
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                var securityRequirement = new OpenApiSecurityRequirement() {
                  {securityScheme, new string[] { }}
              };

                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(securityRequirement);


            });



            services.AddDbContext<SGIApplicationDataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SGIApplication")).EnableSensitiveDataLogging());
            services.Configure<JwtOption>(Configuration.GetSection("Jwt"));
            services.Configure<AppOption>(Configuration.GetSection("AppOption"));
            //services.AddScoped<IServiceBase<Category>, ServiceBase<Category>>();
            services.AddAutoMapper(typeof(OrganizationProfile));
            services.AddControllers();

            services.AddControllers();
            services.AddScoped<IUnitOfWork, SGIAppUnitOfWork>();
            services.AddScoped<IRepository<Incidencia>, Repository<Incidencia>>();
            services.AddScoped<IRepository<Dealer>, Repository<Dealer>>();

            services.AddScoped<IServiceBase<Incidencia>, ServiceBase<Incidencia>>();
            services.AddScoped<IServiceBase<Dealer>, ServiceBase<Dealer>>();
            services.AddScoped<IRepository<Permission>, Repository<Permission>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<RolePermission>, Repository<RolePermission>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();


            services.AddScoped<IServiceBase<User>, ServiceBase<User>>();
            services.AddScoped<IServiceBase<Permission>, ServiceBase<Permission>>();
            services.AddScoped<IServiceBase<Role>, ServiceBase<Role>>();
            services.AddScoped<IServiceBase<RolePermission>, ServiceBase<RolePermission>>();
            services.AddScoped<IServiceBase<UserRole>, ServiceBase<UserRole>>();
            //services.AddHttpClient<IAuthService, AuthService>(client =>
            //{
            //    client.BaseAddress = new Uri(Configuration["Jwt:BaseAddress"]);
            //});

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddCors(o => o.AddPolicy("MyPolicy", b =>
            {
                b.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
            }));

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseAllElasticApm(Configuration);
                UpdateDatabase(app);
            }

            //Enables JWT authentication
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGI-WebApp Api V1");

                c.DocumentTitle = "SGI-WebApp";
                c.DocExpansion(DocExpansion.None);
            });


        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                      .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SGIApplicationDataContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

    }
}
