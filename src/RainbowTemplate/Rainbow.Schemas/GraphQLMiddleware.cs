using System;
using System.IO;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rainbow.Schemas
{
    public class GraphQLRequest
    {
        public string Query { get; set; }

        public JObject Variables { get; set; }
    }

    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GraphQLMiddleware
    {
        public GraphQLMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor)
        {
            Next = next;
            Writer = writer;
            Executor = executor;
        }

        private RequestDelegate Next { get; }

        private IDocumentWriter Writer { get; }

        private IDocumentExecuter Executor { get; }

        public async Task InvokeAsync(HttpContext httpContext, ISchema schema, IServiceProvider serviceProvider)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/Rainbow")
                && string.Equals(httpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                using var streamReader = new StreamReader(httpContext.Request.Body);
                var body = await streamReader.ReadToEndAsync();

                var request = JsonConvert.DeserializeObject<GraphQLRequest>(body);

                var result = await Executor.ExecuteAsync(doc =>
                {
                    doc.Schema = schema;
                    doc.Query = request.Query;
                    doc.Inputs = request.Variables.ToInputs();

                    doc.Listeners.Add(serviceProvider
                       .GetRequiredService<DataLoaderDocumentListener>());
                }).ConfigureAwait(false);

                var json = await Writer.WriteToStringAsync(result);
                await httpContext.Response.WriteAsync(json);
            }
            else
                await Next(httpContext);
        }
    }


    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GraphQLMiddleware>();
        }

        public static IServiceCollection AddGraphQL(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<RainbowQuery>();
            services.AddScoped<RainbowMutation>();

            services.AddScoped<ISchema, RainbowSchema>();

            return services;
        }
    }
}