using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Xml.Linq;
using ТигранянЭС.Projects.EmployeeLib;

namespace Interface
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee("aboba", DateTime.Today, 'M', (decimal)2032, Education.SECONDARY, CurrentPosition.SENIOR));

            dataGrid.ItemsSource = employees;
        }
    }
}
