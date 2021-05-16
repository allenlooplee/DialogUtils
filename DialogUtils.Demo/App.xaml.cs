using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using DialogUtils;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using DialogUtils.Demo.ViewModels;
using DialogUtils.Demo.Views;

namespace DialogUtils.Demo
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

            services.AddTransient<EditContactViewModel>();
            services.AddTransient<EditContactView>();

            return services.BuildServiceProvider();
        }

        private static IDialogHostService ConfigureDialogHostService(IServiceProvider services)
        {
            var dialogHostService = new DialogHostService(services);

            dialogHostService.Configure<EditContactViewModel, EditContactView>();

            return dialogHostService;
        }
    }
}
