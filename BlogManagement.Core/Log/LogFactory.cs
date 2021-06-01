using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace BlogManagement.Core
{
    public class LogFactory
    {
        public static Logger GetLogger()
        {
            string serilogOutputTemplate =
                "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" +
                new string('-', 50);

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File($"{AppContext.BaseDirectory}Logs\\log.log", LogEventLevel.Information,
                    serilogOutputTemplate, rollingInterval: RollingInterval.Day).CreateLogger();
        }

    }
}
