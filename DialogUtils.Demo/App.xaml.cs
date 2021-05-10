using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using DialogUtils;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

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

            services.AddDialogHostService();
            services.AddTransient<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
