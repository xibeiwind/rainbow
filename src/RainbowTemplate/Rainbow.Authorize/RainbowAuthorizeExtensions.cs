using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Rainbow.Authorize
{
    public static class RainbowAuthorizeExtensions
    {
        public static IServiceCollection AddRainbowAuthorize(this IServiceCollection services)
        {
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

            return services;
        }

        public static void UseRainbowAuthorize(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}