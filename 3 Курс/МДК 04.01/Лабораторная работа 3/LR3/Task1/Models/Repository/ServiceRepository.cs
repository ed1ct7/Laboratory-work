using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models.Entity;

namespace Task1.Models.Repository
{
    public class ServiceRepository
    {
        private bool _disposed = false;
        private Service _service;

        public ServiceRepository(Service service = null)
        { 
            _service = service ?? new Service();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(_service);
        }
    }
}
