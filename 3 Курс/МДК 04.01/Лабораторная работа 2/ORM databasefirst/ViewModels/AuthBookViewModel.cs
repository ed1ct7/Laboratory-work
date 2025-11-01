using ORM_databasefirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_databasefirst.ViewModels
{
    public class AuthBookViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; 
                OnPropertyChanged();
            }
        }

        private int _authid;
        public int AuthId
        {
            get { return _authid; }
            set
            {
                _authid = value;
                OnPropertyChanged();
            }
        }

        private int _bookid;
        public int BookId
        {
            get { return _bookid; }
            set
            {
                _bookid = value;
                OnPropertyChanged();
            }
        }
    }
}
