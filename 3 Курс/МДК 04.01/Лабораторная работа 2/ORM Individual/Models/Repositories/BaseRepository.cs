using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;
using ORM_Individual.Interfaces;

namespace ORM_Individual.Models.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual void GetAll()
        {
            throw new NotImplementedException();
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
