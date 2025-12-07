using Microsoft.EntityFrameworkCore;
using ORM_Individual.Models.Entities;
using System.Collections.ObjectModel;
using System.Linq;

namespace ORM_Individual.Models.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public override ObservableCollection<Employee> GetAll()
        {
            return new ObservableCollection<Employee>(
                Set.Include(e => e.Position)
                   .AsNoTracking()
                   .ToList());
        }

        public ObservableCollection<Employee> FilterByPositionSalary(ObservableCollection<Employee> entities, decimal salaryFrom)
        {
            return new ObservableCollection<Employee>(
                from employee in entities
                join position in Context.Positions on employee.PositionId equals position.Id
                select employee
            );
        }
    }
}
