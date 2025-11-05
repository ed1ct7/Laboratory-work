using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeValues();
            Load();
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
            Auth user2 = new Auth { Id = 2, Name = "Антон", Age = 19};
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
        void Load()
        {
            var db = TestContext.GetContext();
            var authBooks = db.Auths.Include(a => a.Books).ToList();

            var dataForGrid = authBooks
                .SelectMany(a => a.Books.Select(book => new
                {
                    authId = a.Id,
                    bookId = book.Id,
                }))
                .ToList();
            AuthData.ItemsSource = db.Auths.ToList();
            BookData.ItemsSource = db.Books.ToList();
            AuthAndBooksData.ItemsSource = dataForGrid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestContext db = TestContext.GetContext();
            try
            {
                var book = db.Books.Find(Convert.ToInt32(BookIdTB.Text));
                var auth = db.Auths.Find(Convert.ToInt32(AuthIdTB.Text));

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
    }
}