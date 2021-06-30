using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services.ControllerProjects;
using System.ComponentModel.DataAnnotations;

[assembly: HostingStartup(typeof(ControllerProjectStartup))]
namespace Rainbow.Platform.WebAPP.HostingStartups
{
    /// <summary>
    ///     ControllerProject Hosting Startup
    /// </summary>
    [Display(Name = "ControllerProject Hosting Startup")]
    public class ControllerProjectStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((services) =>
            {
                services.AddScoped<IControllerProjectActionService, ControllerProjectActionService>();
                services.AddScoped<IControllerProjectQueryService, ControllerProjectQueryService>();
            });
        }
    }
}
