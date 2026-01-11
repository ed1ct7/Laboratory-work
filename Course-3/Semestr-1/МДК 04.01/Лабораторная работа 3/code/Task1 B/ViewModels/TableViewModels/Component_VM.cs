using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Component_VM : BaseTable_VM<Component>
    {
        public Component_VM() : base(new ComponentRepository())
        {
        }
    }
}
