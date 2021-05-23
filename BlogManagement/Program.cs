using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BlogManagement.Core;
using Serilog.Sinks.MSSqlServer;

namespace BlogManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connfiguration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            string serilogOutputTemplate =
                "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" +
                new string('-', 50);
            
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.MessageTemplate); //删除标准列
            columnOptions.Properties.ExcludeAdditionalProperties = true; //排除已经自定义列的数据
            columnOptions.AdditionalColumns = new Collection<SqlColumn> //添加自定义列
            {
                new SqlColumn {DataType = SqlDbType.NVarChar, DataLength = 32, ColumnName = "IP"}
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("IP", GetIp())
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File($"{AppContext.BaseDirectory}Logs\\log.log", LogEventLevel.Information,
                    serilogOutputTemplate, rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connfiguration.GetValue<string>("Serilog:WriteTo:2:Args:connectionString"),
                    new MSSqlServerSinkOptions()
                    {
                        TableName = connfiguration.GetValue<string>("Serilog:WriteTo:2:Args:tableName"),
                        BatchPostingLimit = connfiguration.GetValue<int>("Serilog:WriteTo:2:Args:batchPostingLimit"),
                        BatchPeriod = TimeSpan.FromSeconds(5),
                        AutoCreateSqlTable = true
                    }, restrictedToMinimumLevel: LogEventLevel.Information, columnOptions: columnOptions)
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

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog();

                //完全使用配置文件
                //.UseSerilog((context, configure) =>
                //{
                //    configure.ReadFrom.Configuration(context.Configuration);
                //});

        public static string GetIp()
        {
            //return Dns.GetHostEntry(Dns.GetHostName()).
            //    AddressList.FirstOrDefault(p => p.AddressFamily.ToString() == "InterNetwork")?.ToString();
            try
            {

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("10.0.1.20", 1337); // doesnt matter what it connects to 
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    return endPoint.Address.ToString(); //ipv4 
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
