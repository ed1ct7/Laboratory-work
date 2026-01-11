using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class M_RegistrationData : INotifyPropertyChanged
    {
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _secondName;
		public string SecondName
		{
			get { return _secondName; }
			set { _secondName = value; }
		}

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _bornDate;
		public string BornDate
		{
			get { return _bornDate; }
			set { _bornDate = value; }
		}

		private ReliablePassword reliablePassword;
		public ReliablePassword ReliablePassword
		{
			get { return reliablePassword; }
			set { reliablePassword = value; }
		}

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
