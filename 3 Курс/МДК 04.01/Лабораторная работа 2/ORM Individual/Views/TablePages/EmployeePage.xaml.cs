using ORM_Individual.Interfaces;
using System.Windows.Controls;

namespace ORM_Individual.Views.TablePages
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page, ViewCodeBehind
    {
        public EmployeePage()
        {
            InitializeComponent();
        }
        public Control GetDataGrid()
        {
            return DataGridPar;
        }
    }
}
