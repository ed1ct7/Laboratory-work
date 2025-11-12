using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Service_VM : Base_VM
    {
        private Service _service;
        public Service_VM(Service service = null) {
            _service = service ?? new Service(); 
        }
        public int Id { 
            get => _service.Id; set { _service.Id = value; OnPropertyChange(); } 
        }

        public string? Name {
            get => _service.Name; set { _service.Name = value; OnPropertyChange(); }
        }

        public string? Description
        {
            get => _service.Description; set { _service.Description = value; OnPropertyChange(); } 
        }

        public decimal? Price
        {
            get => _service.Price; set { _service.Price = value; OnPropertyChange(); }
        }
    }
}
