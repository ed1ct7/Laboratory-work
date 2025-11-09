using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.Models.Repositories
{
    public class PositionRepository
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Salary { get; set; }

        public string? Duties { get; set; }

        public string? Requirements { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
