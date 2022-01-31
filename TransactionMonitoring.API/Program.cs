using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionMonitoring.Logger;
using TransactionMonitoring.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace TransactionMonitoring.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILoggerHelper _logger = new LoggerHelper();

            try
            {
                _logger.TraceLog("Initializing application...");
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<DbTransactionContext>();

                        //context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        //context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorLog(ex, "An error occured while migrating the database.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex, "Application stopped bacause of exception.");
            }
            finally
            {
                _logger.Shutdown();
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
