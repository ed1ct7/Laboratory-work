using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Customer_VM : BaseTable_VM<Customer>
    {
        public Customer_VM() : base(new CustomerRepository())
        {
        }
    }
}
