using Microsoft.EntityFrameworkCore;
using ORM_Individual.Models.Entities;
using System.Collections.ObjectModel;

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
    }
}
