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

        private string _affirmativeButtonText;
        public string AffirmativeButtonText
        {
            get => _affirmativeButtonText;
            set => SetProperty(ref _affirmativeButtonText, value);
        }

        private bool _isNegativeButtonVisible;
        public bool IsNegativeButtonVisible
        {
            get => _isNegativeButtonVisible;
            set => SetProperty(ref _isNegativeButtonVisible, value);
        }

        private string _negativeButtonText;
        public string NegativeButtonText
        {
            get => _negativeButtonText;
            set => SetProperty(ref _negativeButtonText, value);
        }

        public void Init(
            string message,
            string header = null,
            string affirmativeButtonText = "OK",
            bool isNegativeButtonVisible = false,
            string negativeButtonText = "Cancel")
        {
            Header = header;
            Message = message;
            AffirmativeButtonText = affirmativeButtonText;
            IsNegativeButtonVisible = isNegativeButtonVisible;
            NegativeButtonText = negativeButtonText;
        }
    }
}
