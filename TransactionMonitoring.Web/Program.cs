using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMonitoring.Logger;

namespace TransactionMonitoring.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {
            ILoggerHelper _logger = new LoggerHelper();
            try
            {
                _logger.TraceLog("Initializing application...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex, "Application stopped bacause of exception.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
