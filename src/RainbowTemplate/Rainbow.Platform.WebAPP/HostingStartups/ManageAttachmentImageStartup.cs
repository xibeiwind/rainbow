using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services.AttachmentImages;


[assembly: HostingStartup(typeof(ManageAttachmentImageStartup))]

namespace Rainbow.Platform.WebAPP.HostingStartups
{
    public class ManageAttachmentImageStartup : IHostingStartup
    {
        #region Implementation of IHostingStartup

        /// <summary>
        /// Configure the <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" />.
        /// </summary>
        /// <remarks>
        /// Configure is intended to be called before user code, allowing a user to overwrite any changes made.
        /// </remarks>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IManageAttachmentImageActionService, ManageAttachmentImageActionService>();
                services.AddScoped<IManageAttachmentImageQueryService, ManageAttachmentImageQueryService>();
            });
        }

        #endregion
    }
}