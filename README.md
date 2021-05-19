# DialogUtils

![Poster](https://github.com/allenlooplee/DialogUtils/blob/main/docs/images/poster.png)

Dialogs are a little bit tricky in the world of MVVM because it can be easy to break the basic rule of MVVM by introducing dialogs inside view models. For example, you want to show a dialog when a user clicks a button which is typically data bound to a command of a view model, and the code to show the dialog is within the implementation of that command. If you create an instance of the dialog within that code, you're making your view model aware of a view, and hence the break of the basic rule of MVVM.

There's no simple way to deal with dialogs in MVVM. [ReactiveUI](https://www.reactiveui.net/)'s solution is to abstract the dialogs as [interactions](https://www.reactiveui.net/docs/handbook/interactions/). [Material Design In XAML Toolkit](http://materialdesigninxaml.net/)'s solution is [DialogHost](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Dialogs). I'm using the latter in [Aclass for Windows](https://www.aketang.cn/) but extend it a little bit to make it easy to work with [Microsoft MVVM Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) and [Microsoft Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)/[.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host).

## Super Quick Start

If you're going to start a new project, it is highly recommended to use the [VSIX template installer](https://github.com/allenlooplee/DialogUtils/tree/main/DialogUtilsProjectTemplateInstaller) to install [a clean project template](https://github.com/allenlooplee/DialogUtils/tree/main/DialogUtilsProjectTemplate) for your Visual Studio.

1. Download the VSIX template installer from the [latest release](https://github.com/allenlooplee/DialogUtils/releases), and run it on your machine.
2. Open Visual Studio and search "Material Design WPF App with DialogUtils" in project templates.
3. Create a project with the "Material Design WPF App with DialogUtils" project template.
4. Hit F5 to run the app.

Note that the project template further leverages Material Design Extensions for [MaterialWindow and AppBar](https://spiegelp.github.io/MaterialDesignExtensions/#documentation/materialwindow).

## Getting Started

If you're going to use DialogUtils in an existing project that already has Material Design In XAML Toolkit [installed and configured](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Getting-Started), below is the guide.

1. Search and install DialogUtils in NuGet Package Manager.
2. In MainWindow.xaml, insert a `<DialogHost/>` between `<Window/>` and it's original child.

```XML
<Window>
    <md:DialogHost Identifier="MainHost">
        <!-- original child -->
    </md:DialogHost>
</Window>
```

3. Register DialogHostService using the AddDialogHostService extension method and MainViewModel in App.xaml.cs. If you're using .NET Generic Host, you can use the UseDialogHostService extension method instead on the IHostBuilder object.

```C#
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
```

4. Setup MainViewModel as MainWindow's DataContext. If you're using .NET Generic Host, you might already have both MainViewModel and MainWindow registered in the container. In this case you can inject MainViewModel in MainWindow's constructor.

```C#
public MainWindow()
{
    InitializeComponent();

    DataContext = Ioc.Default.GetService<MainViewModel>();
}
```

5. Inject IDialogHostService in MainViewModel's constructor, save it as a private member for showing dialogs within any command implementation.

```C#
public class MainViewModel : ObservableObject
{
    private IDialogHostService _dialogHostService;

    private ICommand _showMessageCommand;
    public ICommand ShowMessageCommand => _showMessageCommand ?? (_showMessageCommand = new RelayCommand(ShowMessageImpl));
    private async void ShowMessageImpl()
    {
        await _dialogHostService.ShowMessageAsync(
            dialogIdentifier: "MainHost",
            message: "Hello, World");
    }

    public MainViewModel(IDialogHostService dialogHostService)
    {
        _dialogHostService = dialogHostService;
    }
}
```

## Thanks

* [Material Design In XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
* [Microsoft MVVM Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/main/Microsoft.Toolkit.Mvvm)
* [Microsoft Dependency Injection](https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.DependencyInjection)
* [Material Design Extensions](https://github.com/spiegelp/MaterialDesignExtensions)
* [XamlBehaviors for WPF](https://github.com/microsoft/XamlBehaviorsWpf)
