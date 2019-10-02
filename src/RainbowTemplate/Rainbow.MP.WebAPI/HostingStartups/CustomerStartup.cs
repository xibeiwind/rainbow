using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.MP.WebAPI.HostingStartups;
using Rainbow.Services.CustomerInfos;


[assembly: HostingStartup(typeof(CustomerStartup))]
namespace Rainbow.MP.WebAPI.HostingStartups
{
    public class CustomerStartup : IHostingStartup
    {
        #region Implementation of IHostingStartup

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<ICustomerIdentityService, CustomerIdentityService>();
                services.AddScoped<ICustomerAccountService, CustomerAccountService>();

                services.AddScoped<ICustomerInfoQueryService, CustomerInfoQueryService>();
            });
        }

        #endregion
    }
}
