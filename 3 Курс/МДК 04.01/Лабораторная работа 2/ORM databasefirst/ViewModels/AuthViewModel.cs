using ORM_databasefirst.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ORM_databasefirst.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Book_> _books;
        public ObservableCollection<Book_> Books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged(); }
        }
    }
}
