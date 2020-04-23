using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Rainbow.Platform.Authorize;

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;

namespace Rainbow.Platform.WebAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration, Environment);

            services.AddRainbowAuthorize();

#if (EnableSwagger)
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Rainbow API", Version = "v1"});
                options.IncludeXmlComments(Path.Combine(ApplicationEnvironment.ApplicationBasePath,
                    "Rainbow.Platform.Controllers.xml"));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
            });
#endif
            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new DefaultContractResolver(),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            services.AddMvc(options =>
            {
                //var jsonOutputFormatter = new JsonOutputFormatter(jsonSerializerSettings, ArrayPool<char>.Shared);
                //options.OutputFormatters.Insert(0, jsonOutputFormatter);

                var formatter =
                    new NewtonsoftJsonOutputFormatter(serializerSettings, ArrayPool<char>.Shared, options);
                options.OutputFormatters.Insert(0, formatter);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                var formatter =
                    new NewtonsoftJsonOutputFormatter(serializerSettings, ArrayPool<char>.Shared, options);
                options.OutputFormatters.Insert(0, formatter);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            //services.AddGraphQL();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

#if (EnableSwagger)
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "Rainbow API V1");
            });
#endif

            app.RainbowInit().GetAwaiter().GetResult();

            app.UseRainbowAuthorize();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //app.UseSignalR(builder =>
            //{
            //    // todo: set SignalR here
            //});

            //app.UseGraphQL();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
                spa.Options.StartupTimeout = TimeSpan.FromMinutes(2);
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}