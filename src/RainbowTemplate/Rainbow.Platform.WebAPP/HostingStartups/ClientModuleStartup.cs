using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services.ClientModules;

[assembly: HostingStartup(typeof(ClientModuleStartup))]

namespace Rainbow.Platform.WebAPP.HostingStartups
{
    /// <summary>
    ///     ClientModule Hosting Startup
    /// </summary>
    [Display(Name = "ClientModule Hosting Startup")]
    public class ClientModuleStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IClientModuleActionService, ClientModuleActionService>();
                services.AddScoped<IClientModuleQueryService, ClientModuleQueryService>();
            });
        }
    }
}