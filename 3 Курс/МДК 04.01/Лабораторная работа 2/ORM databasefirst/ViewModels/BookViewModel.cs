using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace ORM_databasefirst.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private int _countpage;
        public int CountPage
        {
            get { return _countpage; }
            set { _countpage = value; OnPropertyChanged(); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Auths> _auths;
        public ObservableCollection<Auths> Auths
        {
            get { return _auths; }
            set { _auths = value; OnPropertyChanged(); }
        }
    }
}
