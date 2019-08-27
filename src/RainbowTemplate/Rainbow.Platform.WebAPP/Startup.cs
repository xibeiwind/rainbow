﻿using System.Buffers;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rainbow.Authorize;
using Rainbow.Schemas;
using Swashbuckle.AspNetCore.Swagger;

namespace Rainbow.Platform.WebAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration, Environment);

            services.AddRainbowAuthorize();

#if (EnableSwagger)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Rainbow API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(ApplicationEnvironment.ApplicationBasePath, "Rainbow.Platform.Controllers.xml"));
            });
#endif

            services.AddMvc(options =>
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver(),
                    DateParseHandling = DateParseHandling.DateTimeOffset,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
                };
                var jsonOutputFormatter = new JsonOutputFormatter(jsonSerializerSettings, ArrayPool<char>.Shared);
                options.OutputFormatters.Insert(0, jsonOutputFormatter);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSignalR(options => { }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver(),
                    DateParseHandling = DateParseHandling.DateTimeOffset,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
                };
            });

            services.AddGraphQL();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Rainbow API V1"); });
#endif

            app.RainbowInit().GetAwaiter().GetResult();

            app.UseRainbowAuthorize();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSignalR(builder =>
            {
                // todo: set SignalR here
            });

            app.UseGraphQL();

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

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
