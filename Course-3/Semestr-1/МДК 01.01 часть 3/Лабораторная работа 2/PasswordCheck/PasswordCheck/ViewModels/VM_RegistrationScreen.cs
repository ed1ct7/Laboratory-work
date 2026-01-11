using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Models;
using ViewModels.Commands;

namespace ViewModels
{
    public class VM_RegistrationScreen : VM_Base
    {
        private ReliablePassword _ReliablePassword;
        private M_RegistrationData _RegistrationData;
        public VM_RegistrationScreen() {
            _RegistrationData = new M_RegistrationData();
            _ReliablePassword = new ReliablePassword();
            RegistrateCommand = new RelayCommand(Registrate);
        }

        public ICommand RegistrateCommand { get; }
        private void Registrate()
        {
            if (!((string.IsNullOrWhiteSpace(Name) ||
                    string.IsNullOrWhiteSpace(SecondName) ||
                    string.IsNullOrWhiteSpace(UserName) ||
                    string.IsNullOrWhiteSpace(BornDate) ||
                    string.IsNullOrWhiteSpace(ReliablePassword))))
                    {
                    string[] Info = new string[] {
                    Name,SecondName,UserName,BornDate.ToString(),BornDate.ToString().Substring(2)
                    };
                PasswordError = _ReliablePassword.Registrate(ReliablePassword, Info);
            }
            else
            {
                PasswordError = "Не все поля заполнены";
            }
        }

        public string Name
        {
            get { return _RegistrationData.Name; }
            set { _RegistrationData.Name = value;
                OnPropertyChanged();
            }
        }
        public string SecondName
        {
            get { return _RegistrationData.SecondName; }
            set { _RegistrationData.SecondName = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get { return _RegistrationData.UserName; }
            set { _RegistrationData.UserName = value;
                OnPropertyChanged();
            }
        }
        public string BornDate
        {
            get {
                return _RegistrationData.BornDate; 
            }
            set {
                _RegistrationData.BornDate = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string ReliablePassword
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged();
            }
        }

        private string _ErrorMessage;
        public string PasswordError
        {
            get { return _ErrorMessage; }
            set {
                _ErrorMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
