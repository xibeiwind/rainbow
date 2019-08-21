using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Data;
using Rainbow.Models;
using Rainbow.Services;
using Rainbow.Services.Users;
using Rainbow.Services.Utils;
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
            services.AddSingleton<SecurityUtil>();

            services.AddDbContext<RainbowDbContext>(opts => { opts.UseMySql(configuration.GetConnectionString("RainbowDB")); });

            services.AddSingleton<IEntityRegisterService, RainbowEntityRegisterService>();

            services.AddSingleton(new ProjectSettings("Rainbow", new DirectoryInfo(environment.ContentRootPath).Parent.FullName));

            services.RegisterEasyNetQ(configuration.GetSection("EventBusConfig").Get<EventBusConfig>());
            services.AddHttpContextAccessor();
            services.AddSingleton(new ConnectionSettings(configuration.GetConnectionString("RainbowDB")));
            services.AddScoped<IConnectionFactory, MySqlConnectionFactory>();

            services.AddScoped<IUserAccountService, UserAccountService>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IViewModelDisplayQueryService, ViewModelDisplayQueryService>();


#if (EnableIdentity)

            services.AddScoped<IRoleService, RoleService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = new JwtSettings();
            configuration.Bind("JwtSettings", jwtSettings);

            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddScoped<ICustomerServiceManageService, CustomerServiceManageService>();

            services.AddTransient<IClaimsTransformation, RainbowClaimsTransformation>();
            services.AddAuthentication(options =>
                {
                    //认证middleware配置
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(new RainbowSecurityTokenValidator());
                });
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

                var dbContext = GetService<RainbowDbContext>();
                dbContext.Database.EnsureCreated();


#if (EnableIdentity)
                app.UseAuthentication();
                var service = GetService<IRoleService>();
                await service.Init();

                var customerServiceManageService = GetService<ICustomerServiceManageService>();
                await customerServiceManageService.InitBuildCustomerService();
#endif


            }

            return app;
        }
    }
}