using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.TableViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.Views.TablePages
{
    /// <summary>
    /// Interaction logic for PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Page
    {
        public PositionPage()
        {
            InitializeComponent();
            DataContext = new Position_VM();
        }

        private Position_VM ViewModel => (Position_VM)DataContext;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
            {
                return;
            }

            if (e.Row.Item is Position position)
            {
                ViewModel.SaveRow(position);
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || PositionDataGrid.SelectedItem is not Position position)
            {
                return;
            }

            ViewModel.DeleteRow(position);
        }
    }
}
