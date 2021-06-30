using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Rainbow.MP.Authorize;

using System;
using System.Buffers;
using System.IO;

namespace Rainbow.MP.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration, Environment);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = " Rainbow MP WebAPI", Version = "v1"});
                options.IncludeXmlComments(Path.Combine(ApplicationEnvironment.ApplicationBasePath,
                    "Rainbow.MP.Controllers.xml"));
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddMvc(options =>
                     {
                         options.EnableEndpointRouting = false;
                     })
                    .AddNewtonsoftJson(options =>
                     {
                         options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                         options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                         options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                         options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                         options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
                         options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                     })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddRainbowAuthorize();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.CosyPresentInit().Wait();

            app.UseRainbowAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });


            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=Index}/{id?}");
            //});
            app.UseMvcWithDefaultRoute();
        }
    }
}