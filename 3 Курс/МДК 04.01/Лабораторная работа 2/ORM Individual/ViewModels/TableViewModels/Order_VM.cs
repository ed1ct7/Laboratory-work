using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Order_VM : BaseTable_VM<Order>
    {
        public Order_VM() : base(new OrderRepository())
        {
        }
    }
}
