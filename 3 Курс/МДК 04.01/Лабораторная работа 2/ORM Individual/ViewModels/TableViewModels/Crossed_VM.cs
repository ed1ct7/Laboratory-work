using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Crossed_VM : BaseTable_VM<Component>
    {
        public Crossed_VM() : base(new ComponentRepository())
        {
        }
    }
}
