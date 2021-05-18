using DialogUtils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DialogUtilsProjectTemplate
{
    class MainViewModel : ObservableObject
    {
        private const string MainDialogHostIdentifier = "MainHost";
        private IDialogHostService _dialogHostService;

        private ICommand _showMessageCommand;
        public ICommand ShowMessageCommand => _showMessageCommand ?? (_showMessageCommand = new RelayCommand(ShowMessageImpl));
        private async void ShowMessageImpl()
        {
            await _dialogHostService.ShowMessageAsync(
                dialogIdentifier: MainDialogHostIdentifier,
                message: "Hello, world!",
                isNegativeButtonVisible: false);
        }

        public MainViewModel(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;
        }
    }
}
