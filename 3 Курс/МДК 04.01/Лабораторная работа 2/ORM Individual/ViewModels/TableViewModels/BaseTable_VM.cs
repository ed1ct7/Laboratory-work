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
        public ICommand DeleteRowCommand { get; }
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

            SaveRowCommand = new RelayCommand(parameter => SaveRow(parameter as T));
            DeleteRowCommand = new RelayCommand(parameter => DeleteRow(parameter as T));
        }
        public void RowEditEnding(object parameter)
        {
            if (parameter is DataGridRowEditEndingEventArgs e)
            {
                e.Row.BindingGroup?.CommitEdit();
                if (e.EditAction == DataGridEditAction.Commit && (e.Row.DataContext is IEntity entity))
                {
                    Source.Add((T)entity);

                    try
                    {
                        Repository.Add((T)entity);
                        //        dataRow["id"] = corent.Id;
                        //bool isNewRow = e.Row. == DataRowState.Added ||
                        //                dataRow.RowState == DataRowState.Detached ||
                        //                dataRow.IsNull("id") ||
                        //                dataRow["id"] == DBNull.Value ||
                        //                Convert.ToInt32(dataRow["id"]) == 0;

                        //    if (isNewRow && (entity is IEntity corent))
                        //    {
                        //        Repository.Add(entity);
                        //        dataRow["id"] = corent.Id;
                        //    }
                        //    else
                        //    {
                        //        Repository.Update(entity);
                        //    }
                        //    Dispatcher dispatcher = System.Windows.Application.Current.Dispatcher;
                        //    Action myAction = delegate ()
                        //    {
                        //        RefreshDataTable(Repository);
                        //    };
                        //    dispatcher.BeginInvoke(myAction, DispatcherPriority.ApplicationIdle);
                    }
                    catch (Exception ex) { }
                    //if (entity is DataRowView dataRowView)
                    //{
                    //    Dispatcher dispatcher = System.Windows.Application.Current.Dispatcher;
                    //    Action myAction = delegate ()
                    //    {
                    //        ProcessRowEdit(dataRowView.Row);
                    //    };
                    //    dispatcher.BeginInvoke(myAction, DispatcherPriority.Background);
                    //}
                }
            }
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
        private void RefreshDataTable(dynamic repository)
        {
            var newDataTable = repository.GetAll();
            Source = newDataTable;
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
