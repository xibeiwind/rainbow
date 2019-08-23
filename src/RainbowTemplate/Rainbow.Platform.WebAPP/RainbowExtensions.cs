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
using Rainbow.Services.Utils;
using Yunyong.Cache; 
using Yunyong.Cache.Register;
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
            var cfg = configuration.Get<CacheServiceConfig>("CacheServiceConfig");
            services.AddSingleton(cfg);
            services.RegisterRedisCache(cfg);

            services.AddSingleton<SecurityUtil>();

            services.AddDbContext<RainbowDbContext>(opts =>
            {
                opts.UseMySql(configuration.GetConnectionString("RainbowDB"));
            });

            services.AddSingleton<IEntityRegisterService, RainbowEntityRegisterService>();

            services.AddSingleton(new ProjectSettings("Rainbow",
                new DirectoryInfo(environment.ContentRootPath).Parent.FullName));

            services.RegisterEasyNetQ(configuration.GetSection("EventBusConfig").Get<EventBusConfig>());
            services.AddHttpContextAccessor();
            services.AddSingleton(new ConnectionSettings(configuration.GetConnectionString("RainbowDB")));
            services.AddScoped<IConnectionFactory, MySqlConnectionFactory>();

            services.AddScoped<IUserAccountService, UserAccountService>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IViewModelDisplayQueryService, ViewModelDisplayQueryService>();


            services.AddScoped<IRoleService, RoleService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = new JwtSettings();
            configuration.Bind("JwtSettings", jwtSettings);

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICustomerServiceManageService, CustomerServiceManageService>();

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

                var dbContext = GetService<RainbowDbContext>();
                dbContext.Database.EnsureCreated();


                var service = GetService<IRoleService>();
                await service.Init();

                var customerServiceManageService = GetService<ICustomerServiceManageService>();
                await customerServiceManageService.InitBuildCustomerService();
            }

            return app;
        }
    }
}