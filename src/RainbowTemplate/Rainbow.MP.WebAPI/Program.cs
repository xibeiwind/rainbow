using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Rainbow.MP.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("CacheSettings.json");
                    config.AddJsonFile("EventBusConfig.json");
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        config.AddJsonFile("CacheSettings.Development.json");
                    }
                })
                .UseStartup<Startup>();
    }
}
