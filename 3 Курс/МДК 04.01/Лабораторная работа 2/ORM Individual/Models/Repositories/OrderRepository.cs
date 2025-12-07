using Microsoft.EntityFrameworkCore;
using ORM_Individual.Models.Entities;
using System.Collections.ObjectModel;

namespace ORM_Individual.Models.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public override ObservableCollection<Order> GetAll()
        {
            return new ObservableCollection<Order>(
                Set.Include(o => o.Customer)
                   .Include(o => o.Employee)
                   .Include(o => o.Component1)
                   .Include(o => o.Component2)
                   .Include(o => o.Component3)
                   .Include(o => o.Service1)
                   .Include(o => o.Service2)
                   .Include(o => o.Service3)
                   .AsNoTracking()
                   .ToList());
        }
    }
}
