using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace DialogUtils.Dialogs
{
    public class InputDialogViewModel : ObservableRecipient
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

        private string _input;
        public string Input
        {
            get => _input;
            set
            {
                if (SetProperty(ref _input, value))
                {
                    OnPropertyChanged(nameof(HasInput));
                }
            }
        }

        public bool HasInput => !string.IsNullOrEmpty(Input);

        private string _affirmativeButtonText;
        public string AffirmativeButtonText
        {
            get => _affirmativeButtonText;
            set => SetProperty(ref _affirmativeButtonText, value);
        }

        private string _negativeButtonText;
        public string NegativeButtonText
        {
            get => _negativeButtonText;
            set => SetProperty(ref _negativeButtonText, value);
        }

        public void Init(
            string message,
            string input = null,
            string header = null,
            string affirmativeButtonText = "OK",
            string negativeButtonText = "Cancel")
        {
            Header = header;
            Message = message;
            Input = input;
            AffirmativeButtonText = affirmativeButtonText;
            NegativeButtonText = negativeButtonText;
        }
    }
}
