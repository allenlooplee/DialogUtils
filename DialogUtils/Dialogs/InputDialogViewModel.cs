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

        public void Init(string message, string input = null, string header = null)
        {
            Header = header;
            Message = message;
            Input = input;
        }
    }
}
