using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DialogUtils
{
    public static class DialogHostServiceExtensions
    {
        public static IServiceCollection AddDialogHostService(this IServiceCollection services, Func<IServiceProvider, IDialogHostService> factory)
        {
            if (factory is null)
            {
                services.AddSingleton<IDialogHostService, DialogHostService>();
            }
            else
            {
                services.AddSingleton(factory);
            }

            return services;
        }

        public static IHostBuilder UseDialogHostService(this IHostBuilder builder, Func<IServiceProvider, IDialogHostService> factory)
        {
            builder.ConfigureServices(services => services.AddDialogHostService(factory));

            return builder;
        }
    }
}
