# DialogUtils

Dialogs are a little bit tricky in the world of MVVM because it can be easy to break the basic rule of MVVM by introducing dialogs inside view models. For example, you want to show a dialog when a user clicks a button which is typically data bound to a command of a view model, and the code to show the dialog is within the implementation of that command. If you create an instance of the dialog within that code, you're making your view model aware of a view, and hence the break of the basic rule of MVVM.

There's no simple way to deal with dialogs in MVVM. [ReactiveUI](https://www.reactiveui.net/)'s solution is to abstract the dialogs as [interactions](https://www.reactiveui.net/docs/handbook/interactions/). [Material Design In XAML Toolkit](http://materialdesigninxaml.net/)'s solution is [DialogHost](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Dialogs). I'm using the latter in [Aclass for Windows](https://www.aketang.cn/) but extend it a little bit to make it easy to work with [Microsoft MVVM Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) and [Microsoft Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)/[.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host).

## Super Quick Start

If you're going to start a new project, it is highly recommended to use the [VSIX template installer](https://github.com/allenlooplee/DialogUtils/tree/main/DialogUtilsProjectTemplateInstaller) to install [a clean project template](https://github.com/allenlooplee/DialogUtils/tree/main/DialogUtilsProjectTemplate) for your Visual Studio.

1. Download the VSIX template installer from the [latest release](https://github.com/allenlooplee/DialogUtils/releases), and run it on your machine.
2. Open Visual Studio and search "Material Design WPF App with DialogUtils" in project templates.
3. Create a project with the "Material Design WPF App with DialogUtils" project template.
4. Hit F5 to run the app.

## Getting Started

1. Create a WPF app. You can create a netcoreapp3.1 WPF project and re-target it to net48 by editing the .csproj file.
2. Search and install DialogUtils in NuGet Package Manager. This will also install its dependencies such as Material Design In XAML Toolkit.
3. Get your WPF app ready for Material Design according to [Super Quick Start](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Super-Quick-Start).
4. In MainWindow.xaml, replace the child element of `<Window/>` with the below code. Note that ShowMessageCommand will be defined in the next step.

```XML
<md:DialogHost Identifier="MainHost">
    <Grid>
        <Button
            Command="{Binding ShowMessageCommand}"
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            Content="Show Message" />
    </Grid>
</md:DialogHost>
```

5. Create the below MainViewModel class and use Visual Studio to automatically add the corresponding namespaces.

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
            message: "Hello, World",
            isNegativeButtonVisible: true);
    }

    public MainViewModel(IDialogHostService dialogHostService)
    {
        _dialogHostService = dialogHostService;
    }
}
```

6. In MainWindow.xaml.cs, get an instance of MainViewModel from the container and assign it to DataContext in the constructor.

```C#
public MainWindow()
{
    InitializeComponent();

    DataContext = Ioc.Default.GetService<MainViewModel>();
}
```

7. Register DialogHostService and MainViewModel in App.xaml.cs.

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

8. Finally, hit F5 to run your app and click the Show Message button. You'll see the "Hello, World" message.

## Thanks

* [Material Design In XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
* [Microsoft MVVM Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/main/Microsoft.Toolkit.Mvvm)
* [Microsoft Dependency Injection](https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.DependencyInjection)
* [Material Design Extensions](https://github.com/spiegelp/MaterialDesignExtensions) (used in Demo project)
* [XamlBehaviors for WPF](https://github.com/microsoft/XamlBehaviorsWpf) (used in Demo project)
