using DialogUtils.Demo.ViewModels;
using DialogUtils.Dialogs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace DialogUtils.Demo
{
    class MainViewModel : ObservableObject
    {
        private const string DialogHostIdentifier = "MainHost";
        private IDialogHostService _dialogHostService;

        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                if (SetProperty(ref _message, value))
                {
                    ShowMessageCommand.NotifyCanExecuteChanged();
                    ShowInputCommand.NotifyCanExecuteChanged();
                }
            }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private RelayCommand _showMessageCommand;
        public RelayCommand ShowMessageCommand => _showMessageCommand ?? (_showMessageCommand = new RelayCommand(ShowMessageImpl, () => !string.IsNullOrEmpty(Message)));
        private async void ShowMessageImpl()
        {
            var result = await _dialogHostService.ShowMessageAsync(DialogHostIdentifier, Message, Header, isNegativeButtonVisible: true);
            Result = result ? "Submitted" : "Cancelled";
        }

        private RelayCommand _showInputCommand;
        public RelayCommand ShowInputCommand => _showInputCommand ?? (_showInputCommand = new RelayCommand(ShowInputImpl, () => !string.IsNullOrEmpty(Message)));
        private async void ShowInputImpl()
        {
            Result = await _dialogHostService.ShowInputAsync(DialogHostIdentifier, Message, null, Header);
        }

        private bool _cancellable;
        public bool Cancellable
        {
            get => _cancellable;
            set => SetProperty(ref _cancellable, value);
        }

        private ICommand _showProgressCommand;
        public ICommand ShowProgressCommand => _showProgressCommand ?? (_showProgressCommand = new RelayCommand(ShowProgressImpl));
        private async void ShowProgressImpl()
        {
            var vm = _dialogHostService.ShowProgressAsync(
                DialogHostIdentifier,
                isIndeterminate: Cancellable,
                cancellable: Cancellable);

            if (Cancellable)
            {
                vm.Cancelled += OnCancelled;
            }
            else
            {
                for (double d = 0; d < 100; d += .5)
                {
                    vm.Value = d;

                    await Task.Delay(10);
                }

                vm.Close();
            }
        }

        private void OnCancelled(object sender, EventArgs e)
        {
            Result = "Cancelled";
        }

        private ContactViewModel _contact;
        public ContactViewModel Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }

        private ICommand _showCustomCommand;
        public ICommand ShowCustomCommand => _showCustomCommand ?? (_showCustomCommand = new RelayCommand(ShowCustomImpl));
        private async void ShowCustomImpl()
        {
            _ = await _dialogHostService.ShowDialogAsync<EditContactViewModel>(
                DialogHostIdentifier,
                vm => vm.Init(Contact));
        }

        private ICommand _clearCommand;
        public ICommand ClearCommand => _clearCommand ?? (_clearCommand = new RelayCommand(ClearImpl));
        private void ClearImpl()
        {
            Header = null;
            Message = null;
            Result = null;
        }

        public MainViewModel(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;

            Contact = new ContactViewModel
            {
                Name = "Loop Lee",
                Age = 17,
                Sex = "Male"
            };
        }
    }
}
