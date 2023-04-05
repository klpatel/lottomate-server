using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace LotoMate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IWebHost hostBuilder;

            hostBuilder = CreateHostBuilder(args).Build();

            var logger = hostBuilder.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("LotoMate-Server Program : Running the host now..");

            hostBuilder.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            //.UseUrls("http://localhost:5002")
            .ConfigureLogging(builder =>
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(Path.Combine(Environment.CurrentDirectory, "Logs", "LotoMate-Log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

                builder.AddSerilog(Log.Logger);
            });
    }
}
