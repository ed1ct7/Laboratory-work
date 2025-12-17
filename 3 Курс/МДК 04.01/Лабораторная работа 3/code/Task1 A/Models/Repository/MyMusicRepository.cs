using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models.Entity;

namespace Task1.Models.Repository
{
    public class MyMusicRepository : IDisposable
    {
        private bool _disposed = false;
        private MyMusic _music;

        public MyMusicRepository(MyMusic music = null)
        { 
            _music = music ?? new MyMusic();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(_music);
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
