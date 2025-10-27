using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteApp;

namespace ORM_databasefirst.ViewModels
{
    public class MainViewModel
    {
                
        public void Load(object Entity)
        {
            var db = ApplicationContext.GetContext();
            db.Database.EnsureCreated();
        }
    }

}
