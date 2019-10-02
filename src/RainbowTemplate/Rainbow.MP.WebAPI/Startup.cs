using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rainbow.MP.Authorize;
using Swashbuckle.AspNetCore.Swagger;
using System.Buffers;
using System.IO;

namespace Rainbow.MP.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration, Environment);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = " Rainbow MP WebAPI", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(ApplicationEnvironment.ApplicationBasePath, "Rainbow.MP.Controllers.xml"));
            });

            services.AddMvc(options =>
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver(),
                    DateParseHandling = DateParseHandling.DateTimeOffset,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss"
                };
                var jsonOutputFormatter = new JsonOutputFormatter(jsonSerializerSettings, ArrayPool<char>.Shared);
                options.OutputFormatters.Insert(0, jsonOutputFormatter);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRainbowAuthorize();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.CosyPresentInit().Wait();

            app.UseRainbowAuthorize();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseMvc();
        }
    }
}
