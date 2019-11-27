using System;
using Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance;
namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
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
// namespace API
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             var host = CreateWebHostBuilder(args).Build();
            
//             using (var scope = host.Services.CreateScope())
//             {
//                 var services = scope.ServiceProvider;
//                 try 
                
//                 {
//                     var context = services.GetRequiredService<DataContext>();
//                     //var userManager = services.GetRequiredService<UserManager<AppUser>>();
//                     context.Database.Migrate();
//                    // Seed.SeedData(context, userManager).Wait();
//                 }
//                 catch (Exception ex)
//                 {
//                     var logger = services.GetRequiredService<ILogger<Program>>();
//                     logger.LogError(ex, "An error occured during migration");
//                 }
//             }

//             host.Run();
//         }

//         public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//             WebHost.CreateDefaultBuilder(args)
//                 .UseStartup<Startup>();
//     }
// }
