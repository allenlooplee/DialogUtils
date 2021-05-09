using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace DialogUtils.Dialogs
{
    public class MessageDialogViewModel : ObservableRecipient
    {
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
            set => SetProperty(ref _message, value);
        }

        private bool _isCancelButtonVisible;
        public bool IsCancelButtonVisible
        {
            get => _isCancelButtonVisible;
            set => SetProperty(ref _isCancelButtonVisible, value);
        }

        public void Init(string message, string header = null, bool isCancelButtonVisible = false)
        {
            Header = header;
            Message = message;
            IsCancelButtonVisible = isCancelButtonVisible;
        }
    }
}
