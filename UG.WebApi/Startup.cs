using System.Reflection;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;

using AspNetCore.Identity.MySQLDapper;
using AspNetCore.Identity.MySQLDapper.Model;
using UG.WebApi.Auth;
using UG.Configuration;

namespace UG.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // https://tahirnaushad.com/2017/09/08/asp-net-core-2-0-bearer-authentication/
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityAlgorithmService.signingKey
                    };
            });

            services.AddControllers()
                // https://docs.microsoft.com/ru-ru/ef/core/querying/related-data#related-data-and-serialization
                // https://stackoverflow.com/questions/55666826/where-did-imvcbuilder-addjsonoptions-go-in-net-core-3-0
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );


            services.AddMvc().AddNewtonsoftJson();

            /* Identity services */
            services.AddSingleton<IUserStore<ApplicationUser>, MySQLUserStore>();
            services.AddSingleton<IUserPasswordStore<ApplicationUser>, MySQLUserStore>();
            services.AddSingleton<IUserEmailStore<ApplicationUser>, MySQLUserStore>();
            services.AddSingleton<IRoleStore<ApplicationRole>, MySQLRoleStore>();
            services.AddSingleton<IUserClaimsPrincipalFactory<ApplicationUser>, MySQLUserPrincipalFactory>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(idOpts =>
            {
                idOpts.Password.RequiredLength = 3;
                idOpts.Password.RequireLowercase = false;
                idOpts.Password.RequireUppercase = false;
                idOpts.Password.RequireDigit = false;
                idOpts.Password.RequireNonAlphanumeric = false;
            })
            .AddDefaultTokenProviders();

            // ORM services auto registration
            // https://www.thereformedprogrammer.net/asp-net-core-fast-and-automatic-dependency-injection-setup/
            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetAssembly(typeof(UG.ORM.IUserService)),
                Assembly.GetAssembly(typeof(UG.WebApi.Controllers.AccountController))
            };
            foreach (var assemblyToScan in assembliesToScan)
            {
                services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                    .Where(c => c.GetInterfaces().Contains(typeof(UG.ORM.Base.IRegisteredService)))
                    .AsPublicImplementedInterfaces();
            }

            services.Configure<AspNetCore.Identity.MySQLDapper.Configuration.ConnectionStringsConfiguration>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<ConnectionStringsConfiguration>(Configuration.GetSection("ConnectionStrings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add custom service identification header
            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Service-Name", "UG Web API");
                return next.Invoke();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .WithOrigins(                    
                    "http://localhost:4200"
                )
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader());

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU"),
            };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            // Find the cookie provider with LINQ
            var cookieProvider = localizationOptions.RequestCultureProviders
                .OfType<CookieRequestCultureProvider>()
                .First();
            // Set the new cookie name
            cookieProvider.CookieName = "UserCulture";
            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
