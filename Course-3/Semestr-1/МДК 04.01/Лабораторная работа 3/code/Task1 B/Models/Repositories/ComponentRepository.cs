using Microsoft.EntityFrameworkCore;
using ORM_Individual.Models.Entities;
using System.Collections.ObjectModel;

namespace ORM_Individual.Models.Repositories
{
    public class ComponentRepository : BaseRepository<Component>
    {
        public override ObservableCollection<Component> GetAll()
        {
            return new ObservableCollection<Component>(
                Set.Include(c => c.Type)
                   .AsNoTracking()
                   .ToList());
        }
    }
}
