using Newtonsoft.Json;
using System;
using Task1.Models.Entity;

namespace Task1.Models.Repository
{
    public class MyMusicRepository
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
    }
}
