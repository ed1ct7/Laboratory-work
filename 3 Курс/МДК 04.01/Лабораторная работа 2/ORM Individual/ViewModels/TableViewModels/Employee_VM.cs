using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;
using ORM_Individual.ViewModels.Commands;
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
            PositionSalaryQueryCommand = new RelayCommand(TogglePositionSalaryQuery);
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

        private void TogglePositionSalaryQuery(object parameter)
        {
            _usePositionSalaryQuery = !_usePositionSalaryQuery;

            if (_usePositionSalaryQuery)
            {
                ApplyPositionSalaryQuery();
            }
            else
            {
                LoadSource();
            }
        }

        private void ApplyPositionSalaryQuery()
        {
            if (_usePositionSalaryQuery)
            {
                Source = _employeeRepository.FilterByPositionSalary(Source, PositionSalaryFrom);
            }
        }
    }
}
