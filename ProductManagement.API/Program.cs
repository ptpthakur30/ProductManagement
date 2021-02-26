using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductManagement.DataAccess;
using ProductManagement.Models;

namespace ProductManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            IHost host = CreateHostBuilder(args).Build();
           using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //Get the database context from the service provider
                    var context = services.GetRequiredService<DataContext>();
                    //var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    //context.Database.Migrate();
                    //seeding the activity entity
                    //Seed.SeedData(context,userManager).Wait();
                    
                }
                catch(Exception ex)
                {
                    //get the logger service for the Program class
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex,"An error occure during migration");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(x=>x.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
