using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Position_VM : BaseTable_VM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Salary { get; set; }

        public string? Duties { get; set; }

        public string? Requirements { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public override void InnitializeRep(object rep)
        {
            throw new NotImplementedException();
        }
    }
}
