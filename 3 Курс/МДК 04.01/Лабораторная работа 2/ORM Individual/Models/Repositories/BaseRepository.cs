using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ORM_Individual.Models.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual ObservableCollection<T> GetAll()
        {
            var db = DatabaseContext.GetContext();
            var dbSet = db.Set<T>();
            return new ObservableCollection<T>(dbSet.ToList());
        }
        public virtual void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public virtual void Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
