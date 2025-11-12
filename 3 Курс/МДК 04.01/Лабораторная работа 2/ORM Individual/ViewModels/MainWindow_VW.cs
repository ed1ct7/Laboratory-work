using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.ViewModels
{
    public class MainWindow_VW : Base_VM
    {
        public Service service;
        public MainWindow_VW()
        {
            service = new Service() { 
                
            };
        }
    }
}
