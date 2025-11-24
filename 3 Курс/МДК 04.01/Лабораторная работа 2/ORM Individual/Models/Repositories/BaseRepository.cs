using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Data;

namespace ORM_Individual.Models.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext Context { get; }
        protected DbSet<T> Set { get; }
        protected BaseRepository()
        {
            Context = DatabaseContext.GetContext();
            Set = Context.Set<T>();
        }
        public virtual T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Set.Add(entity);
            Context.SaveChanges();

            return entity;
        }
        public virtual T CreateInstance()
        {
            return Activator.CreateInstance<T>();
        }
        public virtual ObservableCollection<T> GetAll()
        {
            return new ObservableCollection<T>(Set.AsNoTracking().ToList());
        }
        public virtual void Remove(int id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                return;
            }
            Set.Remove(entity);
            Context.SaveChanges();
        }
        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Set.Update(entity);
            Context.SaveChanges();
            return entity;
        }
        public virtual T? FindById(int id)
        {
            return Set.Find(id);
        }
        public virtual T CreateInstanceFromDataRow(DataRow row) { throw new NotImplementedException(); }
    }
}
