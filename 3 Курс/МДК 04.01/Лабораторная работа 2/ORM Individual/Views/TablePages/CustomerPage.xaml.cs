using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.TableViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.Views.TablePages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
            DataContext = new Customer_VM();
        }

        private Customer_VM ViewModel => (Customer_VM)DataContext;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
            {
                return;
            }

            if (e.Row.Item is Customer customer)
            {
                ViewModel.SaveRow(customer);
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || CustomerDataGrid.SelectedItem is not Customer customer)
            {
                return;
            }

            ViewModel.DeleteRow(customer);
        }
    }
}
