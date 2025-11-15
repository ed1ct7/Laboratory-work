using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Individual.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public void Add(T entity);
        public void Remove(int id);
        public void Update(int id, T entity);
        public void GetAll();
    }
    public interface IViewModel
    {

    }
}
