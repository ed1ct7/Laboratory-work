using Microsoft.EntityFrameworkCore;
using Models;
using ORM_DatabaseFirest.Models;
using ORM_DatabaseFirest.ViewModels.Commands;
using System.Windows.Input;

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

            AuthAndBook authAndBook = new AuthAndBook { AuthId = 1, BookId = 1 };

            db.Auths.AddRange(user1, user2);
            db.Books.AddRange(book1, book2);

            var bookz = db.Books.Find(Convert.ToInt32(1));
            var authz = db.Auths.Find(Convert.ToInt32(1));
            authz.Books.Add(bookz);

            db.SaveChanges();
        }
        public ICommand AddDataCommand { get; set; }
        public virtual void AddData(object parameter) {; }
    }
}
