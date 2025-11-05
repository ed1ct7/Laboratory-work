using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ORM_DatabaseFirest.Models;
using System.Windows;

namespace ORM_DatabaseFirest.ViewModels
{
    public class AuthBookViewModel : MainViewModel, Tables
    {
        public AuthBookViewModel() : base(){ 
            Load();
        }
        public void Load()
        {
            var db = TestContext.GetContext();
            var authBooks = db.Auths.Include(a => a.Books).ToList();

            var dataForGrid = authBooks
                .SelectMany(a => a.Books.Select(book => new AuthAndBook
                {
                    AuthId = a.Id,
                    BookId = book.Id,
                }))
                .ToList();

            AuthAndBookData = new ObservableCollection<AuthAndBook>(dataForGrid);
        }

        #region AddProperties
        private int _authId;
        public int AuthId
        {
            get { return _authId; }
            set { _authId = value; 
                OnPropertyChanged();
            }
        }
        private int _booksId;
        public int BooksId
        {
            get { return _booksId; }
            set { _booksId = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public override void AddData(object parameter)
        {
            TestContext db = TestContext.GetContext();
            try
            {
                var book = db.Books.Find(Convert.ToInt32(BooksId));
                var auth = db.Auths.Find(Convert.ToInt32(AuthId));

                if (auth == null)
                {
                    MessageBox.Show("Введён несуществующий автор");
                }
                else if (book == null)
                {
                    MessageBox.Show("Введена несуществующая книга");
                }
                else if (auth.Books.Any(b => b.Id == book.Id))
                {
                    MessageBox.Show("Эта книга уже связана с данным автором!");
                }
                else
                {
                    auth.Books.Add(book);
                }
                db.SaveChanges();
                Load();
            }
            catch
            {
                MessageBox.Show("Введено неверное значение", "Ошибка");
            }
        }

        private ObservableCollection<AuthAndBook> _authAndBookData;
        public ObservableCollection<AuthAndBook> AuthAndBookData
        {
            get { return _authAndBookData; }
            set { _authAndBookData = value;
                OnPropertyChanged();
            }
        }
    }
}
