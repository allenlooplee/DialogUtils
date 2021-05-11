using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace DialogUtils.Demo
{
    public class MainViewModel : ObservableObject
    {
        private IDialogHostService _dialogHostService;

        private ICommand _showMessageCommand;
        public ICommand ShowMessageCommand => _showMessageCommand ?? (_showMessageCommand = new RelayCommand(ShowMessageImpl));
        private async void ShowMessageImpl()
        {
            await _dialogHostService.ShowMessageAsync("MainHost", "显示消息对话框", header: "提示", isCancelButtonVisible: true);
        }

        private ICommand _showInputCommand;
        public ICommand ShowInputCommand => _showInputCommand ?? (_showInputCommand = new RelayCommand(ShowInputImpl));
        private async void ShowInputImpl()
        {
            await _dialogHostService.ShowInputAsync("MainHost", "显示输入对话框");
        }

        private ICommand _showProgressCommand;
        public ICommand ShowProgressCommand => _showProgressCommand ?? (_showProgressCommand = new RelayCommand(ShowProgressImpl));
        private void ShowProgressImpl()
        {
            _dialogHostService.ShowProgressAsync("MainHost", true, true);
        }

        public MainViewModel(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;
        }
    }
}
