using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class ComponentType_VM : BaseTable_VM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Component> Components { get; set; } = new List<Component>();

        public override void InnitializeRep(object rep)
        {
            throw new NotImplementedException();
        }
    }
}
