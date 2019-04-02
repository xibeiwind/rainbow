using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yunyong.Core;

namespace Rainbow.Platform.WebAPP
{
    public class RainbowEntityRegisterService : IEntityRegisterService
    {
        public RainbowEntityRegisterService(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        private ILogger Logger { get; }

        public void RegisterEntities(ModelBuilder builder)
        {
            //var types = typeof(Model).Assembly.GetTypes()
            //    .Where(a => a.IsClass && a.IsPublic && !a.IsAbstract && a.IsSubclassOf(typeof(Entity)))
            //    .ToList();

            //foreach (var type in types)
            //{
            //    try
            //    {
            //        builder.Entity(type).ToTable(type.GetCustomAttribute<TableAttribute>().Name);
            //    }
            //    catch (Exception e)
            //    {
            //        Logger?.LogError(e, $"[{type.Name}]\t{e.Message}");
            //        throw;
            //    }
            //}
        }
    }
}