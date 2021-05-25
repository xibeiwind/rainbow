using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services.RoleInfos;

[assembly: HostingStartup(typeof(RoleInfoStartup))]

namespace Rainbow.Platform.WebAPP.HostingStartups
{
    /// <summary>
    ///     RoleInfo Hosting Startup
    /// </summary>
    [Display(Name = "RoleInfo Hosting Startup")]
    public class RoleInfoStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IRoleInfoActionService, RoleInfoActionService>();
                services.AddScoped<IRoleInfoQueryService, RoleInfoQueryService>();
            });
        }
    }
}