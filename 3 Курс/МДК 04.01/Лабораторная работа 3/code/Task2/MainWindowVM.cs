using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task2
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public MainWindowVM()
        {
            Initialize();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _data;
        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        // ===== HTTP + JSON =====
        private async Task<List<Country>> DeserializationAsync()
        {
            string url = "https://restcountries.com/v3.1/name/russia";

            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"HTTP Error: {response.StatusCode}");

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Country>>(json);
        }

        // ===== Инициализация =====
        public async void Initialize()
        {
            try
            {
                List<Country> countries = await DeserializationAsync();

                if (countries == null || countries.Count == 0)
                {
                    Data = "Данные не найдены";
                    return;
                }

                Country russia = countries[0];

                Data =
                    $"Страна: {russia.name.common}\n" +
                    $"Официальное название: {russia.name.official}\n" +
                    $"Столица: {russia.capital[0]}\n" +
                    $"Регион: {russia.region}\n" +
                    $"Подрегион: {russia.subregion}\n" +
                    $"Континент: {russia.continents[0]}\n" +
                    $"Население: {russia.population}\n" +
                    $"Площадь: {russia.area} км²\n" +
                    $"Валюта: {russia.currencies.RUB.name} ({russia.currencies.RUB.symbol})\n" +
                    $"Язык: {russia.languages.rus}\n" +
                    $"Автодвижение: {russia.car.side}\n" +
                    $"Член ООН: {(russia.unMember ? "Да" : "Нет")}\n";
            }
            catch (Exception ex)
            {
                Data = $"Ошибка: {ex.Message}";
            }
        }
    }
}
