using Microsoft.EntityFrameworkCore;
using Models;
using ORM_DatabaseFirest.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            InitializeValues();
            AddDataCommand = new RelayCommand(AddData);
        }
        private void InitializeValues()
        {
            var db = TestContext.GetContext();
            db.Database.EnsureCreated();

            foreach (var auth in db.Auths.Include(a => a.Books))
            {
                auth.Books.Clear();
            }

            foreach (var book in db.Books.Include(b => b.Auths))
            {
                book.Auths.Clear();
            }

            db.SaveChanges();

            foreach (var i in db.Auths)
            {

                db.Auths.Remove(i);
            }
            Auth user1 = new Auth { Id = 1, Name = "Эдик", Age = 42 };
            Auth user2 = new Auth { Id = 2, Name = "Антон", Age = 19 };
            foreach (var i in db.Books)
            {
                db.Books.Remove(i);
            }
            Book book1 = new Book { Id = 1, Title = "Поле с плотностью 4 см", CountPage = 1674 };
            Book book2 = new Book { Id = 2, Title = "Ну оно опять не работает", CountPage = 54 };

            db.Auths.AddRange(user1, user2);
            db.Books.AddRange(book1, book2);
            db.SaveChanges();
        }
        public ICommand AddDataCommand { get; set; }
        public virtual void AddData(object parameter) {; }

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
