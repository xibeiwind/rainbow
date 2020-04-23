using System;
using Microsoft.Extensions.DependencyInjection;
using Yunyong.EventBus;

namespace Rainbow.EventHandlers
{
    public static class EventHandlerRegister
    {
        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services)
        {
            return services;
        }

        public static void EventBusSubscribe(this IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var eventBus = scope.ServiceProvider.GetService<IEventBus>();
        }
    }
}