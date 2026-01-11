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
using Models;
using SQLiteApp;

namespace ORM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }
    public void Load()
        {
            User user1 = new User { Name = "Иванов Иван", Age = 37 };
            User user2 = new User { Name = "Петров Петр", Age = 29 };
            var db = ApplicationContext.GetContext();
            db.Database.EnsureCreated();
            db.Users.AddRange(user1, user2);
            db.SaveChanges();
            data.ItemsSource = db.Users.ToList();
        }
    } 
}