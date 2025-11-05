using Models;
using ORM_DatabaseFirest.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            AddDataCommand = new RelayCommand(AddData);
        }

        public ICommand AddDataCommand;
        private void AddData(object parameter)
        {

        }

		private ObservableCollection<Auth> _authdata;
		public ObservableCollection<Auth> AuthsData
		{
			get { return _authdata; }
			set { _authdata = value; }
		}

        private ObservableCollection<Auth> _bookdata;
        public ObservableCollection<Auth> BooksData
        {
            get { return _bookdata; }
            set { _bookdata = value; }
        }

        private ObservableCollection<Auth> _authbooksdata;
        public ObservableCollection<Auth> AuthBooksData
        {
            get { return _authbooksdata; }
            set { _authbooksdata = value; }
        }

    }
}
