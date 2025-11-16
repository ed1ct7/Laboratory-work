using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Service_VM : BaseTable_VM<Service>
    {
        public Service_VM() : base(new ServiceRepository())
        {
        }
    }
}
