using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Position_VM : BaseTable_VM <PositionRepository>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Salary { get; set; }

        public string? Duties { get; set; }

        public string? Requirements { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
