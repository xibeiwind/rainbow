using System.Buffers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rainbow.Schemas;
using Swashbuckle.AspNetCore.Swagger;

namespace Rainbow.Platform.WebAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);

#if (EnableSwagger)
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Rainbow API", Version = "v1" }); });
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

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

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
