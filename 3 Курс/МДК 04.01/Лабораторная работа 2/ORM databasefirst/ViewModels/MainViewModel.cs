using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SQLiteApp;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using ORM_databasefirst.ViewModels.Command;

namespace ORM_databasefirst.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() { 
            
        }
        public void Load(object Entity)
        {
            //var db = ApplicationContext.GetContext();
            //db.Database.EnsureCreated();
            //InsertCommand = new RelayCommand(Insert);
        }
        private ICommand InsertCommand { get; set; }
        private void Insert(object parametr) { 
            
        }
    }
}
