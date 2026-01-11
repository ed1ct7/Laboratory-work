using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class ComponentType_VM : BaseTable_VM<ComponentType>
    {
        public ComponentType_VM() : base(new ComponentTypeRepository())
        {
        }
    }
}
