using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Position_VM : BaseTable_VM<Position>
    {
        public Position_VM() : base(new PositionRepository())
        {
        }
    }
}
