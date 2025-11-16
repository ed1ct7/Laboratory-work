using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public abstract class BaseTable_VM<T> : Base_VM where T : class
    {
        private static bool _databaseInitialized;
        private ObservableCollection<T> _source = new();

        protected IRepository<T> Repository { get; }

        public ICommand SaveRowCommand { get; }

        public ICommand DeleteRowCommand { get; }

        public ObservableCollection<T> Source
        {
            get => _source;
            private set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        protected BaseTable_VM(IRepository<T> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EnsureDatabase();
            LoadSource();

            SaveRowCommand = new RelayCommand(parameter => SaveRow(parameter as T));
            DeleteRowCommand = new RelayCommand(parameter => DeleteRow(parameter as T));
        }

        public void SaveRow(T? entity)
        {
            if (entity == null)
            {
                return;
            }

            var id = GetEntityId(entity);
            if (id == 0 || Repository.FindById(id) == null)
            {
                Repository.Add(entity);
            }
            else
            {
                Repository.Update(entity);
            }

            LoadSource();
        }

        public void DeleteRow(T? entity)
        {
            if (entity == null)
            {
                return;
            }

            var id = GetEntityId(entity);
            if (id == 0)
            {
                Source.Remove(entity);
                return;
            }

            Repository.Remove(id);
            LoadSource();
        }

        protected void LoadSource()
        {
            Source = Repository.GetAll();
        }

        private static void EnsureDatabase()
        {
            if (_databaseInitialized)
            {
                return;
            }

            var context = DatabaseContext.GetContext();
            context.Database.EnsureCreated();
            _databaseInitialized = true;
        }

        private static int GetEntityId(T entity)
        {
            var propertyInfo = entity.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Type {entity.GetType().Name} must expose an Id property");
            }

            var value = propertyInfo.GetValue(entity);
            if (value == null)
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }
    }
}
