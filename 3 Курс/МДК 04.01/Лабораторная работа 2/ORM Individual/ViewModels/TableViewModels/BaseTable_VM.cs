using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public abstract class BaseTable_VM<T> : Base_VM where T : class
    {
        private static bool _databaseInitialized;
        public ObservableCollection<T> _source = new();
        protected IRepository<T> Repository { get; }
        public ICommand RowEditEndingCommand { get; }
        public ICommand SaveRowCommand { get; }
        public ICommand DeleteRowsCommand { get; }
        public ICommand DeleteTableCommand { get; }
        public ObservableCollection<T> Source
        {
            get => _source;
            set
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

            RowEditEndingCommand = new RelayCommand(RowEditEnding);
            DeleteTableCommand = new RelayCommand(DeleteTable);
            DeleteRowsCommand = new RelayCommand(DeleteRows);
        }
        private void DeleteTable(object parameter)
        {

        }
        public void RowEditEnding(object parameter)
        {
            if (parameter is DataGridRowEditEndingEventArgs e)
            {
                e.Row.BindingGroup?.CommitEdit();
                if (e.EditAction == DataGridEditAction.Commit && (e.Row.DataContext is IEntity entity))
                {
                    foreach (IEntity row in Repository.GetAll())
                    {
                        if (row.Id == entity.Id)
                        {
                            Repository.Update((T)entity);
                            return;
                        }
                    }
                    Repository.Add((T)entity);
                }
            }
        }
        public void DeleteRows(object parameter)
        {
            if (parameter is KeyEventArgs e)
            {
                var grid = (DataGrid)e.Source;

                var toDelete = grid.SelectedItems.Cast<T>().ToList();

                foreach (T item in toDelete) { 
                    Source.Remove(item);
                    
                    if(item is IEntity entity)
                    {
                        Repository.Remove(entity.Id);
                    }
                }
            }
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
