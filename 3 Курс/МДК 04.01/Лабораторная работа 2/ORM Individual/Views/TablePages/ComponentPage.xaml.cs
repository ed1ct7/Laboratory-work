using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.TableViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.Views.TablePages
{
    /// <summary>
    /// Interaction logic for ComponentPage.xaml
    /// </summary>
    public partial class ComponentPage : Page
    {
        public ComponentPage()
        {
            InitializeComponent();
            DataContext = new Component_VM();
        }

        private Component_VM ViewModel => (Component_VM)DataContext;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
            {
                return;
            }

            if (e.Row.Item is Component component)
            {
                ViewModel.SaveRow(component);
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || ComponentDataGrid.SelectedItem is not Component component)
            {
                return;
            }

            ViewModel.DeleteRow(component);
        }
    }
}
