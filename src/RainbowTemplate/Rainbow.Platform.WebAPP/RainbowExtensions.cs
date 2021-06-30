using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Rainbow.Common;
using Rainbow.Common.Configs;
using Rainbow.Data;
using Rainbow.Platform.WebAPP.Services;
using Rainbow.Services;
using Rainbow.Services.ClientModules;
using Rainbow.Services.ControllerProjects;
using Rainbow.Services.Users;
using Rainbow.Services.Utils;
using Rainbow.ViewModels.ClientModules;
using Rainbow.ViewModels.ControllerProjects;

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
        public static IServiceCollection RegisterServices(
                this IServiceCollection services,
                IConfiguration configuration,
                IWebHostEnvironment environment
            )
        {
            var cfg = configuration.Get<CacheServiceConfig>("CacheServiceConfig");
            services.AddSingleton(cfg);
            services.RegisterRedisCache(cfg);

            services.AddSingleton<SecurityUtil>();
            services.AddSingleton(new WebPathConfig {WebRootPath = environment.WebRootPath});

            services.AddDbContext<RainbowDbContext>(opts =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
                opts.UseMySql(configuration.GetConnectionString("RainbowDB"), serverVersion);
            });

            services.AddSingleton<IEntityRegisterService, RainbowEntityRegisterService>();

            services.AddSingleton(new ProjectSettings("Rainbow",
                new DirectoryInfo(environment.ContentRootPath).Parent?.FullName));

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

            services.AddSingleton(configuration.Get<JwtSettings>("JwtSettings"));
            services.AddSingleton(configuration.Get<TokenSettings>("TokenSettings"));
            services.AddSingleton(configuration.Get<PictureSettings>("PictureSettings"));

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
                if (!dbContext.Database.CanConnect())
                {
                    dbContext.Database.EnsureCreated();
                    var service = GetService<IRoleService>();
                    await service.Init();
                }

                {
                    var actionService = GetService<IControllerProjectActionService>();
                    var queryService = GetService<IControllerProjectQueryService>();
                    var list = await queryService.GetListAsync();
                    if (!list.Any())
                    {
                        await actionService.CreateAsync(new CreateControllerProjectVM
                        {
                            ProjectName = "Rainbow.Platform.Controllers",
                            ProjectDescription = "Rainbow.Platform.Controllers",
                            IsDefault = true,
                        });
                        await actionService.CreateAsync(new CreateControllerProjectVM
                        {
                            ProjectName = "Rainbow.MP.Controllers",
                            ProjectDescription = "Rainbow.MP.Controllers",
                            IsDefault = true,
                        });
                    }
                }

                {
                    var actionService = GetService<IClientModuleActionService>();
                    var queryService = GetService<IClientModuleQueryService>();
                    var list = await queryService.GetListAsync();
                    if (!list.Any())
                    {
                        await actionService.CreateAsync(new CreateClientModuleVM
                        {
                            Name = "Dashboard",
                            Path = "dashboard",
                            Title = "控制面板",
                            IsCustomLayout = false,
                        });

                        await actionService.CreateAsync(new CreateClientModuleVM
                        {
                            Name = "Admin",
                            Path = "admin",
                            Title = "平台管理",
                            IsCustomLayout = false,
                        });

                        await actionService.CreateAsync(new CreateClientModuleVM
                        {
                            Name = "Manage",
                            Path = "manage",
                            Title = "数据管理",
                            IsCustomLayout = false,
                        });

                        await actionService.CreateAsync(new CreateClientModuleVM
                        {
                            Name = "Auth",
                            Path = "auth",
                            IsCustomLayout = true,
                        });
                    }
                }

                var customerServiceManageService = GetService<ICustomerServiceManageService>();
                await customerServiceManageService.InitBuildCustomerService();
            }

            return app;
        }
    }
}