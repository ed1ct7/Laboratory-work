using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models.Entity;
using Task1.Models.Repository;
using Task1.ViewModels.Commands;

namespace Task1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        MyMusic myCollection;
        private string _serializedString;
        public string SerializedString { get => _serializedString; set { _serializedString = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            myCollection = new MyMusic();
            myCollection.Tracks = new Track[3];
            myCollection.Tracks[0] = new Track()
            {
                Artist = "Artist1",
                Album = "Album1",
                Title = "Title1",
                Year = "2015"
            };
            myCollection.Tracks[1] = new Track()
            {
                Artist = "Artist2",
                Album = "Album2",
                Title = "Title2",
                Year = "2015"
            };
            myCollection.Tracks[2] = new Track()
            {
                Artist = "Artist3",
                Album = "Album3",
                Title = "Title3",
                Year = "2015"
            };

            Serialize = new RelayCommand(execute: () => serialize());
        }

        #region Команды
        public RelayCommand Serialize { get; set; }
        private void serialize()
        {
            using (var _repo = new MyMusicRepository(myCollection)) {
               SerializedString =  _repo.Serialize();
            }
            
        }
        
        #endregion
    }
}
