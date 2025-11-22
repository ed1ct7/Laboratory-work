using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Individual.Interfaces
{
    public interface IRepository<T> where T : class
    {
        ObservableCollection<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Remove(int id);
        T? FindById(int id);
        T CreateInstanceFromDataRow(DataRow dataRow);
    }
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
