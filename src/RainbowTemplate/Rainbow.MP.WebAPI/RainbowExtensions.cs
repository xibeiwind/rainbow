using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Rainbow.Common;
using Rainbow.Common.Configs;
using Rainbow.EventHandlers;
using Rainbow.Services.Users;
using Rainbow.Services.Utils;

using System.Threading.Tasks;

using Yunyong.Cache;
using Yunyong.Cache.Register;
using Yunyong.Core;
using Yunyong.EventBus;
using Yunyong.EventBus.EasyNetQ;
using Yunyong.SqlUtils;

namespace Rainbow.MP.WebAPI
{
    public static class RainbowExtensions
    {
        public static IServiceCollection RegisterServices(
                this IServiceCollection services,
                IConfiguration configuration,
                IWebHostEnvironment environment
            )
        {
            services.AddHttpContextAccessor();
            services.AddSingleton(new ConnectionSettings(configuration.GetConnectionString("RainbowDB")));
            services.AddScoped<IConnectionFactory, MySqlConnectionFactory>();


            var cfg = configuration.Get<CacheServiceConfig>("CacheServiceConfig");
            services.AddSingleton(cfg);
            services.RegisterRedisCache(cfg);

            services.AddSingleton(configuration.Get<JwtSettings>("JwtSettings"));
            services.AddSingleton(configuration.Get<WechatSettings>("WechatSettings"));
            services.AddSingleton(configuration.Get<TokenSettings>("TokenSettings"));
            //services.AddSingleton(configuration.Get<PictureSettings>("PictureSettings"));

            //services.AddSingleton(configuration.Get<BaiduAISettings>("BaiduAISettings"));

            services.AddSingleton<SecurityUtil>();
            services.RegisterEasyNetQ(configuration.GetSection("EventBusConfig").Get<EventBusConfig>());
            services.AddScoped<IRoleService, RoleService>();

            services.RegisterEventHandlers();

            return services;
        }

        public static async Task<IApplicationBuilder> CosyPresentInit(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                T GetService<T>()
                {
                    return scope.ServiceProvider.GetService<T>();
                }

                scope.ServiceProvider.EventBusSubscribe();
            }

            return app;
        }
    }
}