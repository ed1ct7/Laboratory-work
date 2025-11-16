using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.TableViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.Views.TablePages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            DataContext = new Order_VM();
        }

        private Order_VM ViewModel => (Order_VM)DataContext;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
            {
                return;
            }

            if (e.Row.Item is Order order)
            {
                ViewModel.SaveRow(order);
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || OrderDataGrid.SelectedItem is not Order order)
            {
                return;
            }

            ViewModel.DeleteRow(order);
        }
    }
}
