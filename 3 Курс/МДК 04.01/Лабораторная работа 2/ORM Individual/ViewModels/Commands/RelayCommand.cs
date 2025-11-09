using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ORM_Individual.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        public Action _execute;
        public RelayCommand(Action execute) { 
            _execute = execute;
        }
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public void Execute(object? parameter)
        {
            _execute();
        }
    }
}
