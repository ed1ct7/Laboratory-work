using ORM_Individual.Interfaces;
using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;
using ORM_Individual.ViewModels.Commands;
using System.Windows.Input;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Position_VM : BaseTable_VM<Position>
    {
        public Position_VM() : base(new PositionRepository())
        {
            MaxSalaryQueryCommand = new RelayCommand(MaxSalaryQuery);
            MinSalaryQueryCommand = new RelayCommand(MinSalaryQuery);
            IsUnderFunction = false;
        }
        #region Queries
        public ICommand MaxSalaryQueryCommand { get; }
        public ICommand MinSalaryQueryCommand { get; }
        public bool IsUnderFunction { get; set; }
        private void MaxSalaryQuery(object parameter)
        {
            if (Repository is PositionRepository positionrep)
            {
                if (!IsUnderFunction)
                {

                    Source = positionrep.SalaryQueryMax(Source);
                    IsUnderFunction = true;
                }
                else
                {
                    LoadSource();
                }
            }
        }
        private void MinSalaryQuery(object parameter)
        {
            if (Repository is PositionRepository positionrep)
            {
                if (!IsUnderFunction)
                {
                    Source = positionrep.SalaryQueryMin(Source);
                    IsUnderFunction = true;
                }
                else
                {
                    LoadSource();
                    IsUnderFunction = false;
                }
            }
        }
        #endregion
    }
}
