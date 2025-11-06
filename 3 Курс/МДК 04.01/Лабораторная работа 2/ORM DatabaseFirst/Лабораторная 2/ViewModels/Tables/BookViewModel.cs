using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using Interfaces;

namespace ORM_DatabaseFirest.ViewModels
{
    public class BookViewModel : MainViewModel, Tables
    {
        public BookViewModel()
        {
            Load();
        }
        public void Load()
        {
            var db = TestContext.GetContext();
            BooksData = new ObservableCollection<Book>(db.Books.ToList());
        }

        #region AddProperties
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _age;

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        private int _auth_id;

        public int Auth_Id
        {
            get { return _auth_id; }
            set
            {
                _auth_id = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ObservableCollection<Book> _booksData;
        public ObservableCollection<Book> BooksData
        {
            get { return _booksData; }
            set
            {
                _booksData = value;
                OnPropertyChanged();
            }
        }
    }
}
