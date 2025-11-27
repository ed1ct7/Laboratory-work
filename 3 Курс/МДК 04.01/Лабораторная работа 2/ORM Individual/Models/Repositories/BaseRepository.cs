using ORM_Individual.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Data;
using ORM_Individual.Models.Database;
using ORM_Individual.ViewModels;

//3.Сформулировать запросы для заданной предметной области:
//-на выборку(2 запроса с различными условиями), 
//-на использование статистических функций(1 запрос),
//- на соединение таблиц. 


namespace ORM_Individual.Models.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
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

        #region Queries
        public virtual ObservableCollection<T> UseQuery() {
            
            return new ObservableCollection<T>(Set.AsNoTracking().ToList());
        }
        public  ObservableCollection<T> IdQueries(ObservableCollection<T> entities, int IdMoreThan, int IdLessThan)
        {
            return new ObservableCollection<T>(
                (IEnumerable<T>)
                (
                from entity in entities
                where entity.Id >= IdMoreThan && entity.Id <= IdLessThan
                select entity)
                );
        }
        #endregion
    }
}
