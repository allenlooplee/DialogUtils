using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace DialogUtils.Demo
{
    public class MainViewModel : ObservableObject
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
            var result = await _dialogHostService.ShowMessageAsync(DialogHostIdentifier, Message, Header, isCancelButtonVisible: true);
            Result = result ? "Submitted" : "Cancelled";
        }

        private RelayCommand _showInputCommand;
        public RelayCommand ShowInputCommand => _showInputCommand ?? (_showInputCommand = new RelayCommand(ShowInputImpl, () => !string.IsNullOrEmpty(Message)));
        private async void ShowInputImpl()
        {
            Result = await _dialogHostService.ShowInputAsync(DialogHostIdentifier, Message, null, Header);
        }

        private ICommand _showProgressCommand;
        public ICommand ShowProgressCommand => _showProgressCommand ?? (_showProgressCommand = new RelayCommand(ShowProgressImpl));
        private void ShowProgressImpl()
        {
            _dialogHostService.ShowProgressAsync(DialogHostIdentifier, true, true);
        }

        private ICommand _showCustomCommand;
        public ICommand ShowCustomCommand => _showCustomCommand ?? (_showCustomCommand = new RelayCommand(ShowCustomImpl));
        private void ShowCustomImpl()
        {

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
        }
    }
}
