using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string SerilogOutputTemplate = "{NewLine}{NewLine}Date£º{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel£º{Level}{NewLine}Message£º{Message}{NewLine}{Exception}" + new string('-', 50);

            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
             .MinimumLevel.Override("System", LogEventLevel.Information)
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .WriteTo.File($"{AppContext.BaseDirectory}Logs\\log.log", LogEventLevel.Information, SerilogOutputTemplate, rollingInterval: RollingInterval.Day)
             .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog();
    }
}
