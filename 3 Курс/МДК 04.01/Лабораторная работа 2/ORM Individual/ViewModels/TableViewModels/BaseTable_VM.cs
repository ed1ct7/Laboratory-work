using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public abstract class BaseTable_VM<T> : Base_VM where T : class
    {
        private ObservableCollection<T> _source;
        public ObservableCollection<T> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }
        protected IRepository<T> _repository;
        public IRepository<T> Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }
        private void InitializeValues()
        {
            var db = DatabaseContext.GetContext();
            db.Database.EnsureCreated();

            if (!db.Services.Any()) // ← проверка
            {
                Service user1 = new Service { Id = 3, Name = "Эдик", Description = "Aboba", Price = 100 };
                Service user2 = new Service { Id = 4, Name = "Антон", Description = "Aboba", Price = 100 };

                db.Services.AddRange(user1, user2);
                db.SaveChanges();
            }
        }

        protected BaseTable_VM(IRepository<T> repository)
        {
            RowEditEndingCommand = new RelayCommand(RowEditEnding);
            InitializeValues();
            InitializeRep(repository);
        }

        protected void InitializeRep(IRepository<T> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Source = Repository.GetAll();
            OnPropertyChanged(nameof(Source));
        }

        protected DataTable _dataTable;
        public DataTable DataTableC
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged();
            }
        }
        public ICommand CellEditEndingCommand { get; }
        public ICommand RowEditEndingCommand { get; }
        protected void RowEditEnding(object parameter)
        {
            if (parameter is DataGridRowEditEndingEventArgs e)
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    if (e.Row.DataContext is DataRowView dataRowView)
                    {
                        Dispatcher dispatcher = System.Windows.Application.Current.Dispatcher;
                        Action myAction = delegate ()
                        {
                            ProcessRowEdit(dataRowView.Row);
                        };
                        dispatcher.BeginInvoke(myAction, DispatcherPriority.Background);
                    }
                }
            }
        }
        protected void ProcessRowEdit(DataRow dataRow)
        {
            try
            {
                bool isNewRow = dataRow.RowState == DataRowState.Added ||
                                dataRow.RowState == DataRowState.Detached ||
                                dataRow.IsNull("id") ||
                                dataRow["id"] == DBNull.Value ||
                                Convert.ToInt32(dataRow["id"]) == 0;

                dynamic repository = Repository;
                var entity = repository.CreateInstanceFromDataRow(dataRow);

                if (isNewRow)
                {
                    repository.Add(entity);
                    dataRow["id"] = entity.Id;
                }
                else
                {
                    repository.Update(entity);
                }
                Dispatcher dispatcher = System.Windows.Application.Current.Dispatcher;
                Action myAction = delegate ()
                {
                    RefreshDataTable(repository);
                };
                dispatcher.BeginInvoke(myAction, DispatcherPriority.ApplicationIdle);
            }
            catch (Exception ex) { }
        }

        protected void RefreshDataTable(dynamic repository)
        {
            var newDataTable = repository.GetAll();
            DataTableC = newDataTable;
        }
    }
}
