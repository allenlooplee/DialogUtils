using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace DialogUtils.Demo.ViewModels
{
    public class EditContactViewModel : DialogViewModelBase
    {
        private ContactViewModel _contact;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _age;
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _sex;
        public string Sex
        {
            get => _sex;
            set => SetProperty(ref _sex, value);
        }

        protected override void SubmitImpl()
        {
            _contact.Name = Name;
            _contact.Age = Age;
            _contact.Sex = Sex;

            CloseDialog();
        }

        public EditContactViewModel(IDialogHostService dialogHostService)
            : base(dialogHostService)
        {
            
        }

        public void Init(ContactViewModel contact)
        {
            _contact = contact;

            Name = _contact.Name;
            Age = _contact.Age;
            Sex = _contact.Sex;
        }
    }
}
