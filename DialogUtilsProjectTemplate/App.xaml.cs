using DialogUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DialogUtilsProjectTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDialogHostService(ConfigureDialogHostService);

            services.AddTransient<MainViewModel>();

            // Register dialog views and view models here.
            // services.AddTransient<YourDialogViewModel>();
            // services.AddTransient<YourDialogView>();

            return services.BuildServiceProvider();
        }

        private static IDialogHostService ConfigureDialogHostService(IServiceProvider services)
        {
            var dialogHostService = new DialogHostService(services);

            // Configure dialog views and view models here.
            // dialogHostService.Configure<YourDialogViewModel, YourDialogView>();

            return dialogHostService;
        }
    }
}
