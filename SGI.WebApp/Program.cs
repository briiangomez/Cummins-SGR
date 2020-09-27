using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace SGI.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("SGI-WebApp Starting");

                CreateHostBuilder(args).Build().Run();

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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)

               .ConfigureAppConfiguration(c => c.AddEnvironmentVariables(c => c.Prefix = "SGI_"))

               .ConfigureWebHostDefaults(webBuilder =>
               {

                   webBuilder
                   .ConfigureKestrel((context, serverOptions) =>
                   {
                        // Set properties and call methods on serverOptions
                    })
                   .UseStartup<Startup>()
                   .ConfigureLogging(logging =>
                   {
                       logging.ClearProviders();
                       logging.AddConsole();
                       logging.AddDebug();
                   })
                   .UseNLog();

               });
    }
}
