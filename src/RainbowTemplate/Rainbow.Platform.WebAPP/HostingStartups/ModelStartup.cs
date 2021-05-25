using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services.Models;

[assembly: HostingStartup(typeof(ModelStartup))]

namespace Rainbow.Platform.WebAPP.HostingStartups
{
    public class ModelStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IModelActionService, ModelActionService>();
                services.AddScoped<IModelQueryService, ModelQueryService>();
            });
        }
    }
}