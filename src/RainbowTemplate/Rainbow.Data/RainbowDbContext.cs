using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Yunyong.Core;

namespace Rainbow.Data
{
    public class RainbowDbContext : DbContext
    {
        public RainbowDbContext(
                IEnumerable<IEntityRegisterService> services,
                DbContextOptions<RainbowDbContext> options
            ) : base(options)
        {
            Services = services;
        }

        private IEnumerable<IEntityRegisterService> Services { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var service in Services)
                try
                {
                    service.RegisterEntities(builder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }
    }
}