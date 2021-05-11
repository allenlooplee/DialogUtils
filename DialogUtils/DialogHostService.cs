using DialogUtils.Dialogs;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DialogUtils
{
    public class DialogHostService : IDialogHostService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _views = new Dictionary<string, Type>();
        private readonly Dictionary<string, Type> _dialogs = new Dictionary<string, Type>();

        public DialogHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Configure<MessageDialogViewModel, MessageDialogView>();
            Configure<InputDialogViewModel, InputDialogView>();
            Configure<ProgressDialogViewModel, ProgressDialogView>();
        }

        private Type GetViewType(string key)
        {
            Type viewType;
            lock (_views)
            {
                if (!_views.TryGetValue(key, out viewType))
                {
                    throw new ArgumentException($"View not found: {key}. Did you forget to call Configure?");
                }
            }

            return viewType;
        }

        private UserControl GetView(string key)
        {
            var viewType = GetViewType(key);
            return _serviceProvider.GetService(viewType) as UserControl;
        }

        public void Configure<VM, V>()
            where VM : class
            where V : UserControl
        {
            lock (_views)
            {
                var key = typeof(VM).FullName;
                if (_views.ContainsKey(key))
                {
                    throw new ArgumentException($"The key {key} is already configured in DialogHostService");
                }

                var type = typeof(V);
                if (_views.Any(v => v.Value == type))
                {
                    throw new ArgumentException($"This type is already configured with key {_views.First(v => v.Value == type).Key}");
                }

                _views.Add(key, type);
            }
        }

        public async Task ShowDialogAsync<VM>(string dialogIdentifier, Action<VM> init = null)
            where VM : class
        {
            if (_dialogs.TryGetValue(dialogIdentifier, out var vmType) && vmType != null)
            {
                if (vmType == typeof(VM))
                {
                    return;
                }
                else
                {
                    DialogHost.Close(dialogIdentifier);
                }
            }

            _dialogs[dialogIdentifier] = typeof(VM);

            var view = GetView(typeof(VM).FullName);
            await DialogHost.Show(
                content: view,
                dialogIdentifier: dialogIdentifier,
                openedEventHandler: (o, e) =>
                {
                    var viewModel = view.DataContext as VM;

                    if (init != null)
                    {
                        if (viewModel is DialogViewModelBase dialogViewModel)
                        {
                            dialogViewModel.DialogIdentifier = dialogIdentifier;
                        }

                        init.Invoke(viewModel);
                    }
                    else
                    {
                        if (viewModel is DialogViewModelBase dialogViewModel)
                        {
                            dialogViewModel.DialogIdentifier = dialogIdentifier;
                            dialogViewModel.Init();
                        }
                    }
                });

            _dialogs[dialogIdentifier] = null;
        }

        public async Task<bool> ShowMessageAsync(string dialogIdentifier, string message, string header = null, bool isCancelButtonVisible = false)
        {
            if (_dialogs.TryGetValue(dialogIdentifier, out var vmType) && vmType != null)
            {
                DialogHost.Close(dialogIdentifier);
            }

            _dialogs[dialogIdentifier] = typeof(MessageDialogView);

            var view = _serviceProvider.GetService<MessageDialogView>();
            var result = await DialogHost.Show(
                content: view,
                dialogIdentifier: dialogIdentifier,
                openedEventHandler: (o, e) =>
                {
                    var viewModel = view.DataContext as MessageDialogViewModel;
                    viewModel.Init(message, header, isCancelButtonVisible);
                });

            _dialogs[dialogIdentifier] = null;

            return (bool)result;
        }

        public async Task<string> ShowInputAsync(string dialogIdentifier, string message, string input = null, string header = null)
        {
            if (_dialogs.TryGetValue(dialogIdentifier, out var vmType) && vmType != null)
            {
                DialogHost.Close(dialogIdentifier);
            }

            _dialogs[dialogIdentifier] = typeof(InputDialogView);

            var view = _serviceProvider.GetService<InputDialogView>();
            var result = await DialogHost.Show(
                content: view,
                dialogIdentifier: dialogIdentifier,
                openedEventHandler: (o, e) =>
                {
                    var viewModel = view.DataContext as InputDialogViewModel;
                    viewModel.Init(message, input, header);
                });

            _dialogs[dialogIdentifier] = null;

            return (result as string);
        }

        public async void ShowProgressAsync(string dialogIdentifier, bool isIndeterminate = true, bool cancellable = false)
        {
            if (_dialogs.TryGetValue(dialogIdentifier, out var vmType) && vmType != null)
            {
                DialogHost.Close(dialogIdentifier);
            }

            _dialogs[dialogIdentifier] = typeof(ProgressDialogViewModel);

            var view = _serviceProvider.GetService<ProgressDialogView>();
            await DialogHost.Show(
                content: view,
                dialogIdentifier: dialogIdentifier,
                openedEventHandler: (o, e) =>
                {
                    var viewModel = view.DataContext as ProgressDialogViewModel;
                    viewModel.Init(dialogIdentifier, isIndeterminate, cancellable);
                });

            _dialogs[dialogIdentifier] = null;
        }

        public void CloseDialog(string dialogIdentifier)
        {
            if (DialogHost.IsDialogOpen(dialogIdentifier))
            {
                DialogHost.Close(dialogIdentifier);
            }
        }
    }
}
