using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Common;
using Rainbow.Data;
using Rainbow.Services;
using Rainbow.Services.Users;
using Yunyong.Core;
using Yunyong.EventBus;
using Yunyong.EventBus.EasyNetQ;
using Yunyong.SqlUtils;

namespace Rainbow.Platform.WebAPP
{
    public static class RainbowExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,
            IConfiguration configuration, IHostingEnvironment environment)
        {
            services.AddDbContext<RainbowDbContext>(opts => { opts.UseMySql(configuration.GetConnectionString("RainbowDB")); });

            services.AddSingleton<IEntityRegisterService, RainbowEntityRegisterService>();

            services.AddSingleton(new ProjectSettings("Rainbow", new DirectoryInfo(environment.ContentRootPath).Parent.FullName));

            services.RegisterEasyNetQ(configuration.GetSection("EventBusConfig").Get<EventBusConfig>());
            services.AddHttpContextAccessor();
            services.AddSingleton(new ConnectionSettings(configuration.GetConnectionString("RainbowDB")));
            services.AddScoped<IConnectionFactory, MySqlConnectionFactory>();

            services.AddScoped<IUserAccountService,UserAccountService>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IViewModelDisplayQueryService, ViewModelDisplayQueryService>();

#if (EnableIdentity)

#endif
            return services;
        }

        public static async Task<IApplicationBuilder> RainbowInit(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                T GetService<T>()
                {
                    return scope.ServiceProvider.GetService<T>();
                }

#if (EnableIdentity)
                
#endif
                var dbContext = GetService<RainbowDbContext>();
                dbContext.Database.EnsureCreated();
            }

            return app;
        }
    }
}