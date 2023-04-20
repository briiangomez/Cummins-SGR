using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace SGIWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                var host = BuildWebHost(args);

                

                if (args != null && args.Length > 0)
                {
                    logger.Log(NLog.LogLevel.Trace, $"arguments received: { string.Join(' ', args) }");
                }
                
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        => WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c => c.AddCommandLine(args))
                .ConfigureKestrel((opt) => {

                })
                 .UseStartup<Startup>()
                 .ConfigureLogging(logging =>
                 {
                     logging.ClearProviders();
                 })
                .UseNLog(); // NLog: setup NLog for Dependency injection
                   

        public static IWebHost BuildWebHost(string[] args) =>
               CreateWebHostBuilder(args)
                .Build();
    }
}
