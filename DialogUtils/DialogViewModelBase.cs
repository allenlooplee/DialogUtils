using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace DialogUtils
{
    public class DialogViewModelBase : ObservableRecipient
    {
        protected IDialogHostService _dialogHostService;

        public string DialogIdentifier { get; set; }

        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private RelayCommand _submitCommand;
        public RelayCommand SubmitCommand => _submitCommand ?? (_submitCommand = new RelayCommand(SubmitImpl, CanSubmit));
        protected virtual bool CanSubmit()
        {
            return true;
        }
        protected virtual void SubmitImpl()
        {
            
        }

        public DialogViewModelBase(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;
        }

        public virtual void Init()
        {
            
        }

        public void CloseDialog()
        {
            _dialogHostService.CloseDialog(DialogIdentifier);
        }
    }
}
