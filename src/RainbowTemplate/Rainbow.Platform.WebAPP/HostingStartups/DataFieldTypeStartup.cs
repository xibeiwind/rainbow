using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services.DataFieldTypes;

[assembly: HostingStartup(typeof(DataFieldTypeStartup))]
namespace Rainbow.Platform.WebAPP.HostingStartups
{
	/// <summary>
    ///     DataFieldType Hosting Startup
    /// </summary>
    [Display(Name = "DataFieldType Hosting Startup")]
    public class DataFieldTypeStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((services) =>
            {
                services.AddScoped<IDataFieldTypeActionService, DataFieldTypeActionService>();
                services.AddScoped<IDataFieldTypeQueryService, DataFieldTypeQueryService>();
            });
        }
    }
}
