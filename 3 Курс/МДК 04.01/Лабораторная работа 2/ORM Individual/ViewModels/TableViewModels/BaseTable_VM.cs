using ORM_Individual.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public abstract class BaseTable_VM : Base_VM
    {
        protected object _repository;
        public object Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }
        protected void InitializeRep(object rep)
        {
            Repository = rep;
        }
        
        public BaseTable_VM() {

            RowEditEndingCommand = new RelayCommand(RowEditEnding);

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
