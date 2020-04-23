using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Rainbow.MP.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                 {
                     config.AddJsonFile("CacheSettings.json");
                     config.AddJsonFile("EventBusConfig.json");
                     if (context.HostingEnvironment.IsDevelopment())
                     {
                         config.AddJsonFile("CacheSettings.Development.json");
                     }
                 })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}