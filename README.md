# DialogUtils

Dialogs are a little bit tricky in the world of MVVM because it can be easy to break the basic rule of MVVM by introducing dialogs inside view models. For example, you want to show a dialog when a user clicks a button which is typically data bound to a command of a view model, and the code to show the dialog is within the implementation of that command. If you create an instance of the dialog within that code, you're making your view model aware of a view, and hence the break of the basic rule of MVVM.

There's no simple way to deal with dialogs in MVVM. [ReactiveUI](https://www.reactiveui.net/)'s solution is to abstract the dialogs as [interactions](https://www.reactiveui.net/docs/handbook/interactions/). [Material Design In XAML Toolkit](http://materialdesigninxaml.net/)'s solution is [DialogHost](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Dialogs). I'm using the latter in [Aclass for Windows](https://www.aketang.cn/) but extend it a little bit to make it easy to work with [Microsoft MVVM Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) and [Microsoft Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)/[.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host).

## Setup

## Usage

## Thanks

* [Material Design In XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
* [Microsoft MVVM Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/main/Microsoft.Toolkit.Mvvm)
* [Microsoft Dependency Injection](https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.DependencyInjection)
* [Material Design Extensions](https://github.com/spiegelp/MaterialDesignExtensions) (used in Demo project)
* [XamlBehaviors for WPF](https://github.com/microsoft/XamlBehaviorsWpf) (used in Demo project)
