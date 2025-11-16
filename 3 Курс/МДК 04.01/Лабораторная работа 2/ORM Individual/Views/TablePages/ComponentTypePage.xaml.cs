using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.TableViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.Views.TablePages
{
    /// <summary>
    /// Interaction logic for ComponentTypePage.xaml
    /// </summary>
    public partial class ComponentTypePage : Page
    {
        public ComponentTypePage()
        {
            InitializeComponent();
            DataContext = new ComponentType_VM();
        }

        private ComponentType_VM ViewModel => (ComponentType_VM)DataContext;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
            {
                return;
            }

            if (e.Row.Item is ComponentType type)
            {
                ViewModel.SaveRow(type);
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || ComponentTypeDataGrid.SelectedItem is not ComponentType type)
            {
                return;
            }

            ViewModel.DeleteRow(type);
        }
    }
}
