using Newtonsoft.Json;
using System;
using Task1.Models.Entity;

namespace Task1.Models.Repository
{
    public class ServiceRepository : IDisposable
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

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        public void Dispose(bool isDisposing)
        {
           if (!_disposed)
            {
                if (isDisposing)
                {
                    //NNN
                }
            }
        }
        #endregion
    }
}
