using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Service_VM : BaseTable_VM
    {
        public Service_VM() {
            object repository = new ServiceRepository();
            InitializeRep(repository);
        }
    }
    public class ServiceSingleElement : Service_VM
    {
        private Service _service;
        public ServiceSingleElement(Service service = null)
        {
            _service = service ?? new Service();
        }
        public int Id
        {
            get => _service.Id; set { _service.Id = value; OnPropertyChanged(); }
        }

        public string? Name
        {
            get => _service.Name; set { _service.Name = value; OnPropertyChanged(); }
        }

        public string? Description
        {
            get => _service.Description; set { _service.Description = value; OnPropertyChanged(); }
        }

        public decimal? Price
        {
            get => _service.Price; set { _service.Price = value; OnPropertyChanged(); }
        }
    }

}
