using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DialogUtils
{
    public interface IDialogHostService
    {
        void Configure<VM, V>()
            where VM : class
            where V : UserControl;

        Task ShowDialogAsync<VM>(string dialogIdentifier, Action<VM> init = null);
        void CloseDialog(string dialogIdentifier);

        Task<bool> ShowMessageAsync(string dialogIdentifier, string message, string header = null, bool isCancelButtonVisible = false);
        Task<string> ShowInputAsync(string dialogIdentifier, string message, string input = null, string header = null);
        void ShowProgressAsync(string dialogIdentifier, bool isIndeterminate = true, bool cancellable = false);
    }
}
