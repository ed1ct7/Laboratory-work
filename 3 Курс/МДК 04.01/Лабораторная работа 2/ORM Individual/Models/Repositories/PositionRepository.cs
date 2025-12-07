using ORM_Individual.Models.Entities;
using System.Collections.ObjectModel;

namespace ORM_Individual.Models.Repositories
{
    public class PositionRepository : BaseRepository<Position>
    {
        public ObservableCollection<Position> SalaryQueryMax(ObservableCollection<Position> entities)
        {
            return new ObservableCollection<Position>(
                from entity in entities
                where entity.Salary == Context.Positions.Select(p => p.Salary).AsEnumerable().Max()
                select entity
                );
        }
        public ObservableCollection<Position> SalaryQueryMin(ObservableCollection<Position> entities)
        {
            return new ObservableCollection<Position>(
                from entity in entities
                where entity.Salary == Context.Positions.Select(p => p.Salary).AsEnumerable().Min()
                select entity
                );
            }   
    }
}
