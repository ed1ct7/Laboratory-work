using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Employee_VM : BaseTable_VM<Employee>
    {
        public Employee_VM() : base(new EmployeeRepository())
        {
        }
    }
}
