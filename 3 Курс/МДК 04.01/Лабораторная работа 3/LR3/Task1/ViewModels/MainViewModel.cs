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
        private Service _service;
        private string _serializedString;
        public string SerializedString { get => _serializedString; set { _serializedString = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            _service = new Service
            {
                Id = 1,
                Name = "Базовая услуга",
                Description = "Пример услуги для сериализации",
                Price = 1500m
            };

            Serialize = new RelayCommand(execute: () => serialize());
        }

        #region Команды
        public RelayCommand Serialize { get; set; }
        private void serialize()
        {
            using (var _repo = new ServiceRepository(_service))
            {
                SerializedString = _repo.Serialize();
            }

        }
        #endregion
    }
}
