using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Customer_VM : BaseTable_VM
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public override void InnitializeRep(object rep)
        {
            throw new NotImplementedException();
        }
    }
}
