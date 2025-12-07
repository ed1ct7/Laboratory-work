using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;
using ORM_DatabaseFirest.ViewModels;

namespace ORM_DatabaseFirest.Views.Pages
{
    /// <summary>
    /// Interaction logic for AuthBookView.xaml
    /// </summary>
    public partial class AuthBookView : Page
    {
        public AuthBookView()
        {
            InitializeComponent();
            this.DataContext = new AuthBookViewModel();
        }
    }
}
