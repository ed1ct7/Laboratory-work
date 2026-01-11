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
using ViewModels;
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
            this.DataContext = new MainViewModel();
            //Load();
        }
        //void Load()
        //{
        //    var db = TestContext.GetContext();
        //    var authBooks = db.Auths.Include(a => a.Books).ToList();

        //    var dataForGrid = authBooks
        //        .SelectMany(a => a.Books.Select(book => new
        //        {
        //            authId = a.Id,
        //            bookId = book.Id,
        //        }))
        //        .ToList();
        //    AuthData.ItemsSource = db.Auths.ToList();
        //    BookData.ItemsSource = db.Books.ToList();
        //    AuthAndBooksData.ItemsSource = dataForGrid;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    TestContext db = TestContext.GetContext();
        //    try
        //    {
        //        var book = db.Books.Find(Convert.ToInt32(BookIdTB.Text));
        //        var auth = db.Auths.Find(Convert.ToInt32(AuthIdTB.Text));

        //        if (auth == null)
        //        {
        //            MessageBox.Show("Введён несуществующий автор");
        //        }
        //        else if (book == null)
        //        {
        //            MessageBox.Show("Введена несуществующая книга");
        //        }
        //        else if (auth.Books.Any(b => b.Id == book.Id))
        //        {
        //            MessageBox.Show("Эта книга уже связана с данным автором!");
        //        }
        //        else
        //        {
        //            auth.Books.Add(book);
        //        }
        //        db.SaveChanges();
        //        Load();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Введено неверное значение", "Ошибка");
        //    }
        //}
    }
}