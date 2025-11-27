using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;
using System.Windows.Input;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Position_VM : BaseTable_VM<Position>
    {
        public Position_VM(IRepository<Position> repository) : base(repository){
            
        }

        #region Queries
        public ICommand UseSalaryQueryCommand { get; }
        public ICommand MaxSalaryQueryCommand { get; }
        public ICommand MinSalaryQueryCommand { get; }
        private void UseSalaryQuery(object parameter)
        {
            if (IsIdQuery == false)
            {
                IsIdQuery = true;
                SalaryQuerySelect();
            }
            else
            {
                IsIdQuery = false;
                LoadSource();
            }
        }
        private void SalaryQuerySelect()
        {
            if (IsIdQuery == true)
            {
                Source = Repository.IdQueries(Source, IdFrom, IdTo);
            }
        }
        public bool IsSalaryQuery;
        
        #endregion
    }
}
