using DialogUtils.Dialogs;
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

        Task<VM> ShowDialogAsync<VM>(string dialogIdentifier, Action<VM> init = null)
            where VM : class;
        void CloseDialog(string dialogIdentifier);

        Task<bool> ShowMessageAsync(
            string dialogIdentifier,
            string message,
            string header = null,
            string affirmativeButtonText = "OK",
            bool isNegativeButtonVisible = false,
            string negativeButtonText = "Cancel");

        Task<string> ShowInputAsync(
            string dialogIdentifier,
            string message,
            string input = null,
            string header = null,
            string affirmativeButtonText = "OK",
            string negativeButtonText = "Cancel");

        ProgressDialogViewModel ShowProgressAsync(
            string dialogIdentifier,
            bool isIndeterminate = true,
            bool cancellable = false,
            string cancelButtonText = "Cancel");
    }
}
