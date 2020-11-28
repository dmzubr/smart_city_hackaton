using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace UG.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogger();
            CreateHostBuilder(args).Build().Run();
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Logger(l => l
                .WriteTo.File("/logs/web-apis/ug-api-{Date}.log"))
                .CreateLogger();

            var contentRoot = "/app";
#if DEBUG
            contentRoot = Directory.GetCurrentDirectory();
#endif

            Log.Logger.Information("ContentRoot is '{ContentRoot}'", contentRoot);
        }        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog();
                });
    }
}
