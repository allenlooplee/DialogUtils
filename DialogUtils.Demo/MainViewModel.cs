using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            await _dialogHostService.ShowMessageAsync("MainHost", "Showing message dialog.");
        }

        public MainViewModel(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;
        }
    }
}
