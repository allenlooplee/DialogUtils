# DialogUtils

![Poster](https://github.com/allenlooplee/DialogUtils/blob/main/docs/images/poster.png)

Dialogs are a little bit tricky in the world of MVVM because it can be easy to break the basic rule of MVVM by introducing dialogs inside view models. For example, you want to show a dialog when a user clicks a button which is typically data bound to a command of a view model, and the code to show the dialog is within the implementation of that command. If you create an instance of the dialog within that code, you're making your view model aware of a view, and hence the break of the basic rule of MVVM.

There's no simple way to deal with dialogs in MVVM. [ReactiveUI](https://www.reactiveui.net/)'s solution is to abstract the dialogs as [interactions](https://www.reactiveui.net/docs/handbook/interactions/). [Material Design In XAML Toolkit](http://materialdesigninxaml.net/)'s solution is [DialogHost](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Dialogs). I'm using the latter in [Aclass for Windows](https://www.aketang.cn/) but extend it a little bit to make it easy to work with [Microsoft MVVM Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) and [Microsoft Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)/[.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host).

## Super Quick Start

If you're going to start a new project, it is highly recommended to use the [VSIX template installer](https://marketplace.visualstudio.com/items?itemName=allenlooplee.dialogutils) to install [a clean project template](https://github.com/allenlooplee/DialogUtils/tree/main/DialogUtilsProjectTemplate) for your Visual Studio.

1. Find and install "dialogutils" in Visual Studio's [Manage Extensions dialog box](https://docs.microsoft.com/en-us/visualstudio/ide/finding-and-using-visual-studio-extensions?view=vs-2019).
2. Search "dialogutils" and [create a new project](https://docs.microsoft.com/en-us/visualstudio/ide/create-new-project?view=vs-2019) with the "Material Design WPF App with DialogUtils" project template in Visual Studio.
3. Hit F5 to run the app.

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

## Usage

1. How to show a message?

The simplest way to do so is call the `ShowMessageAsync` on an instance of `IDialogHostService` usually injected via constructor as below. The `dialogIdentifier` and `message` parameters are required; the others such as `header` are optional.

```C#
await _dialogHostService.ShowMessageAsync(DialogHostIdentifier, Message);
```

2. How to show a message with OK and Cancel buttons and return whether the user clicks the OK button?

Passing true to the `isNegativeButtonVisible` parameter of `ShowMessageAsync` will show both OK and Cancel buttons. The return value will be `true` if the user clicks OK button; otherwise `false`. The text of both buttons can also be customized by using the `affirmativeButtonText` and `negativeButtonText` parameters of `ShowMessageAsync` respectively.

```C#
var result = await _dialogHostService.ShowMessageAsync(DialogHostIdentifier, Message, isNegativeButtonVisible: true);
```

3. How to get input from user?

Call `ShowInputAsync` as below which will show a dialog with a `TextBox` inside. If the user types some text within the `TextBox` and clicks OK button, the method will return that as a `string`; otherwise, `null`. If you want also to show an existing value in the `TextBox`, you can use the third parameter which is currently `null` in the below code.

```C#
var result = await _dialogHostService.ShowInputAsync(DialogHostIdentifier, Message, null, Header);
```

4. How to show a progress dialog?

Call `ShowProgressAsync` as below which will show a small dialog with an indeterminate progress ring inside. You'll need to close the dialog manually by calling `CloseDialog` on an instance of `IDialogHostService` with the same `DialogHostIdentifier`.

```C#
_dialogHostService.ShowProgressAsync(DialogHostIdentifier);
```
If you want also a cancel button on the dialog, you can pass `true` to the `cancellable` parameter. In this case, clicking that cancel button will close the current progress dialog.

`ShowProgressAsync` returns the view model used by the current progress dialog. If you want to change the value of the progress ring, you can pass `false` to the `isIndeterminate` parameter and increase the value of the `Value` property as below. Note that you can also use the `Close` method of the view model to close the current progress dialog.

```C#
var vm = _dialogHostService.ShowProgressAsync(
    DialogHostIdentifier,
    isIndeterminate: false);

for (double d = 0; d < 100; d += .5)
{
    vm.Value = d;
    await Task.Delay(10);
}

vm.Close();
```

5. How to show a custom dialog?

Let's say you have a view named `EditContactView` and a view model named `EditContactViewModel` registered as transient in `App`'s `ConfigureServices` method as below. Note that views are required to inherit from `UserControl`.

```C#
services.AddTransient<EditContactViewModel>();
services.AddTransient<EditContactView>();
```

And tell DialogHostService that they relate to each other via `Configure` method as below. Note that if you create a project with the VSIX template, the `ConfigureDialogHostService` method is already created for you.

```C#
private static IDialogHostService ConfigureDialogHostService(IServiceProvider services)
{
    var dialogHostService = new DialogHostService(services);
    dialogHostService.Configure<EditContactViewModel, EditContactView>();
    return dialogHostService;
}
```

Now you can show it via `ShowDialogAsync` method as below. As you can see, you tell it which view model to use, it'll find the corresponding view to show. You can also initialize the view model by passing a lambda.

```C#
await _dialogHostService.ShowDialogAsync<EditContactViewModel>(
    DialogHostIdentifier,
    vm => vm.Init(Contact));
```

Usually we inject a view model into a view via constructor as below. In this case DialogHostService will use the view model you injected. Otherwise, it will get one from the container and associate it to the view for you.

```C#
public EditContactView(EditContactViewModel viewModel)
{
    InitializeComponent();
    DataContext = viewModel;
}
```

6. How to know if a dialog host is opened or being closed?

You can register `DialogHostMessage` with `WeakReferenceMessenger`'s `Register` method, and then check the `DialogIdentifier` property to see which dialog host is being used. To know whether the dialog host is opened or being closed, you can check the `DialogHostEvent` property to see if it's `DialogHostEvent.Opened` or `DialogHostEvent.Closing` respectively.

## Thanks

* [Material Design In XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
* [Microsoft MVVM Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/main/Microsoft.Toolkit.Mvvm)
* [Microsoft Dependency Injection](https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.DependencyInjection)
* [Material Design Extensions](https://github.com/spiegelp/MaterialDesignExtensions)
* [XamlBehaviors for WPF](https://github.com/microsoft/XamlBehaviorsWpf)
