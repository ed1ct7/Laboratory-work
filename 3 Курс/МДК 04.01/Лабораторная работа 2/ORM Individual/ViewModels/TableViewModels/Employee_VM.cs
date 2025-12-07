using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;
using ORM_Individual.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Employee_VM : BaseTable_VM<Employee>
    {
        private readonly EmployeeRepository _employeeRepository;

        private bool _usePositionSalaryQuery;
        private decimal _positionSalaryFrom;

        public ICommand PositionSalaryQueryCommand { get; }

        public Employee_VM() : base(new EmployeeRepository())
        {
            _employeeRepository = (EmployeeRepository)Repository;
            PositionSalaryQueryCommand = new RelayCommand(PositionSalaryQuery);
        }

        public decimal PositionSalaryFrom
        {
            get => _positionSalaryFrom;
            set
            {
                _positionSalaryFrom = value;
                OnPropertyChanged();
                ApplyPositionSalaryQuery();
            }
        }

        public virtual ObservableCollection<Employee> Source
        {
            get => _source;
            set
            {
                _source = value;
                OnPropertyChanged();

            }
        }

        public DataGridColumn[] temp;

        private void PositionSalaryQuery(object parameter)
        {
            _usePositionSalaryQuery = !_usePositionSalaryQuery;
            var dataGrid = parameter as DataGrid;

            if (temp == null)
            {
                temp = new DataGridColumn[8];
                dataGrid.Columns.CopyTo(temp,0);
            }
            if (_usePositionSalaryQuery)
            {
                dataGrid.AutoGenerateColumns = true;
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
                
                dataGrid.Columns.Clear();
                ApplyPositionSalaryQuery();
            }
            else
            {
                LoadSource();
                dataGrid.AutoGenerateColumns = false;
                foreach (var column in temp)
                {
                    dataGrid.Columns.Add(column);
                }
            }
        }

        private void ApplyPositionSalaryQuery()
        {
            if (_usePositionSalaryQuery)
            {
                var temp = Source;
                Source = _employeeRepository.FilterByPositionSalary(Source, PositionSalaryFrom);
            }
        }
    }
}
