using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Interfaces;

namespace ORM_DatabaseFirest.ViewModels
{
    public class AuthViewModel : MainViewModel, Tables
    {
        public AuthViewModel() { 
            Load();
        }
        public void Load()
        {
            var db = TestContext.GetContext();
            AuthsData = new ObservableCollection<Auth>(db.Auths.ToList());
        }

        #region AddProperties
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private int _countPage;

        public int CountPage
        {
            get { return _countPage; }
            set
            {
                _countPage = value;
                OnPropertyChanged();
            }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private int _book_id;

        public int Book_Id
        {
            get { return _book_id; }
            set
            {
                _book_id = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ObservableCollection<Auth> _authsData;
        public ObservableCollection<Auth> AuthsData
        {
            get { return _authsData; }
            set
            {
                _authsData = value;
                OnPropertyChanged();
            }
        }
    }
}
