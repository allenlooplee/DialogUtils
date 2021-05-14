using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace DialogUtils.Dialogs
{
    public class ProgressDialogViewModel : ObservableRecipient
    {
        private readonly IDialogHostService _dialogHostService;
        private string _dialogIdentifier;

        private bool _isIndeterminate;
        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set => SetProperty(ref _isIndeterminate, value);
        }

        private double _value;
        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _cancellable;
        public bool Cancellable
        {
            get => _cancellable;
            set => SetProperty(ref _cancellable, value);
        }

        private string _cancelButtonText;
        public string CancelButtonText
        {
            get => _cancelButtonText;
            set => SetProperty(ref _cancelButtonText, value);
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(CancelImpl));
        private void CancelImpl()
        {
            _dialogHostService.CloseDialog(_dialogIdentifier);

            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Cancelled;

        public ProgressDialogViewModel(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;
        }

        public void Init(
            string dialogIdentifier,
            bool isIndeterminate,
            bool cancellable = false,
            string cancelButtonText = "Cancel")
        {
            _dialogIdentifier = dialogIdentifier;
            IsIndeterminate = isIndeterminate;
            Value = 0;
            Cancellable = cancellable;
            CancelButtonText = cancelButtonText;
        }

        public void Close()
        {
            _dialogHostService.CloseDialog(_dialogIdentifier);
        }
    }
}
