using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ORM_Individual.Models;
using ORM_Individual.Models.Entities;
using ORM_Individual.ViewModels.Commands;

namespace ORM_Individual.ViewModels
{
    public class MainWindow_VW : Base_VM
    {
        public Service service;
        public MainWindow_VW()
        {
            service = new Service() { 
         
            };
            CanvasRightClickCommand = new RelayCommand(CanvasRightClick);
        }

        public ICommand CanvasRightClickCommand { get; }
        public void CanvasRightClick(object parametr)
        {

        }
    }
}
