using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP;
using Rainbow.Services.Users;

[assembly: HostingStartup(typeof(UserStartup))]

namespace Rainbow.Platform.WebAPP
{
    /// <summary>
    ///     User Hosting Startup
    /// </summary>
    [Display(Name = "User Hosting Startup")]
    public class UserStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IUserActionService, UserActionService>();
                services.AddScoped<IUserQueryService, UserQueryService>();
            });
        }
    }
}