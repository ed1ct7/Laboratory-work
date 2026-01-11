using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Models
{
    public class Password : INotifyPropertyChanged
    {
        protected string correctPassword = "MySecret123!";

        public event PropertyChangedEventHandler? PropertyChanged;

        public Password(string password = "MySecret123!")
        {
            Registrate(password);
        }
        public void Registrate(string newPassword)
        {
            correctPassword = newPassword;
        }
        public virtual string Check(string input)
        {
            return input == correctPassword ? "Пароль верный" : "Пароль неверный";
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ReliablePassword : Password
    {
        private readonly string[] forbiddenPatterns = { "1234567890", "qwertyuyiop[]","asdfghjkl;'","zxcvbnm,./","!@#$%^&*()_+","QWERTYUIOP{}","ASDFGHJKL:","ZXCVBNM<>?" };
        private M_RegistrationData m_RegistrationData;
        private string[] personalInfo;

        private string _passwordErrorMessage;

        public string PasswordErrorMessage
        {
            get { return _passwordErrorMessage; }
            set { _passwordErrorMessage = value; }
        }

        public ReliablePassword(string password, M_RegistrationData m_RegistrationData)
        {
            this.m_RegistrationData = m_RegistrationData;

            this.personalInfo = new string[5] {
                m_RegistrationData.UserName,                        //Ник пользователя
                m_RegistrationData.Name,                            //Имя пользователя
                m_RegistrationData.SecondName,                      //Фамилия пользователя
                m_RegistrationData.BornDate,             //Дата рождения
                m_RegistrationData.BornDate.Substring(2) //Дата рождения (только два последних символа)
            };

            Registrate (password, personalInfo);
        }

        public ReliablePassword()
        {

        }

        private bool ContainsKeyboardSequence(string password)
        {
            string lowerPass = password.ToLower();

            foreach (var pattern in forbiddenPatterns)
            {
                string lowerPattern = pattern.ToLower();
                string reversedPattern = new string(lowerPattern.Reverse().ToArray());

                if (HasSequence(lowerPass, lowerPattern) || HasSequence(lowerPass, reversedPattern))
                    return true;
            }

            return false;
        }

        private bool HasSequence(string text, string pattern)
        {
            for (int seqLen = 4; seqLen <= pattern.Length; seqLen++)
            {
                for (int i = 0; i <= pattern.Length - seqLen; i++)
                {
                    string segment = pattern.Substring(i, seqLen);
                    if (text.Contains(segment))
                        return true;
                }
            }
            return false;
        }

        public string Registrate(string password, string[] personalInfo)
        {
            bool isReliable = true;
            if (password.Length <= 12)
                PasswordErrorMessage = "Меньше 12 символов";

            else if (!password.Any(char.IsUpper))
                PasswordErrorMessage = "Нет прописных символов";

            else if(!password.Any(char.IsLower))
                PasswordErrorMessage = "Нет строчных символов";

            else if(!password.Any(char.IsDigit))
                PasswordErrorMessage = "Нет цифр";

            else if(!Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]"))
                PasswordErrorMessage = "Нет специальных симолов";

            else if (ContainsKeyboardSequence(password))
                PasswordErrorMessage = "Содержит последовательность более 3 символов подряд (например, 1234, qwerty)";

            else if (personalInfo.Any(info => password.ToLower().Contains(info)))
                PasswordErrorMessage = "Содержит личные данные пользователя";

            else
                PasswordErrorMessage = "Пароль принят";

            correctPassword = password;
            return PasswordErrorMessage;
        }
    }
}
