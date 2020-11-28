using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using NetCore.AutoRegisterDi;

using AspNetCore.Identity.MySQLDapper;
using AspNetCore.Identity.MySQLDapper.Model;

using UG.ORM;
using UG.Configuration;
using UG.ORM.Impl;

namespace UG.Utils.Cli
{
    class Program
    {
        private static IConfigurationRoot Configuration;
        private static ServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            await CreateUser();
        }

        private static async Task CreateUser()
        {
            var srv = GetServiceInstance<IUserCreatorService>();
            var userName = "region_manager";
            var pass = "01region_manager01";
            var rolesList = new string[] { "region_manager" };
            await srv.CreateUserRecord(userName, pass, rolesList);
        }

        private static async Task ImportIndexesList()
        {
            var srv = GetServiceInstance<IIndexesDataImportService>();
            var indexesDataFilePath = "E:/proj/hackaton_ug/metadata/показатели-пояснения.tsv";
            await srv.ImportIndexesData(indexesDataFilePath);
        }

        private static async Task ImportCitiesList()
        {
            var srv = GetServiceInstance<ICitiesImportService>();
            var indexesDataFilePath = "E:/proj/hackaton_ug/metadata/cities_list.tsv";
            Log.Information("Cities list import started");
            await srv.ImportCities(indexesDataFilePath);
            Log.Information("Cities list import finished");
        }

        static T GetServiceInstance<T>()
        {
            Console.WriteLine("TRY: Initialise configuration");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            Console.WriteLine("SUCCESS: Initialise configuration");

            ConfigureLogger();

            Log.Logger.Debug("TRY: Configure Dependency injection container");
            ConfigureDI();
            Log.Logger.Debug("SUCCESS: Configure Dependency injection container");

            return _serviceProvider.GetService<T>();
        }

        private static void ConfigureLogger()
        {
            var logsCataloguePath = @"logs";
            if (!Directory.Exists(logsCataloguePath))
                Directory.CreateDirectory(logsCataloguePath);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logsCataloguePath + $"/create-users-cli-{DateTime.Now.Date.ToString()}.log")
                .CreateLogger();
        }

        private static void ConfigureDI()
        {
            var services = new ServiceCollection();

            services.AddLogging();

            services.AddSingleton<IUserStore<ApplicationUser>, MySQLUserStore>();
            services.AddSingleton<IUserPasswordStore<ApplicationUser>, MySQLUserStore>();
            services.AddSingleton<IUserEmailStore<ApplicationUser>, MySQLUserStore>();
            services.AddSingleton<IRoleStore<ApplicationRole>, MySQLRoleStore>();
            services.AddSingleton<IUserClaimsPrincipalFactory<ApplicationUser>, MySQLUserPrincipalFactory>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(idOpts =>
            {
                idOpts.Password.RequiredLength = 6;
                idOpts.Password.RequireLowercase = false;
                idOpts.Password.RequireUppercase = false;
                idOpts.Password.RequireDigit = false;
                idOpts.Password.RequireNonAlphanumeric = false;
            })
            .AddDefaultTokenProviders();

            services.AddSingleton<IUserService, UserServiceImpl>();
            services.AddSingleton<IUserCreatorService, UserCreatorImpl>();

            services.AddSingleton<IIndexesDataImportService, IndexesDataImportServiceImpl>();
            services.AddSingleton<ICitiesImportService, CitiesImportServiceImpl>();

            // ORM services auto registration
            // https://www.thereformedprogrammer.net/asp-net-core-fast-and-automatic-dependency-injection-setup/
            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetAssembly(typeof(UG.ORM.IUserService))                
            };
            foreach (var assemblyToScan in assembliesToScan)
            {
                services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                    .Where(c => c.GetInterfaces().Contains(typeof(UG.ORM.Base.IRegisteredService)))
                    .AsPublicImplementedInterfaces();
            }

            services.Configure<AspNetCore.Identity.MySQLDapper.Configuration.ConnectionStringsConfiguration>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<ConnectionStringsConfiguration>(Configuration.GetSection("ConnectionStrings"));
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
