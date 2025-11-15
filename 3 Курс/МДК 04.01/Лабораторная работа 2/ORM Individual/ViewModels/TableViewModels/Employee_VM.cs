using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Employee_VM : BaseTable_VM<EmployeeRepository>
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public int? Age { get; set; }

        public bool? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? PassportData { get; set; }

        public int? PositionId { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual Position? Position { get; set; }
    }
}
