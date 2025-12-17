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

        private async Task<List<Country>> DeserializationAsync()
        {
            try
            {
                string url = "https://restcountries.com/v3.1/name/russia";

                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"HTTP Error: {response.StatusCode}");

                string json = await response.Content.ReadAsStringAsync();

                try
                {
                    return JsonConvert.DeserializeObject<List<Country>>(json);
                }
                catch (JsonException jex)
                {
                    Data = "Ошибка десериализации: " + jex.Message;
                    return null;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Data = "Ошибка HTTP: " + httpEx.Message;
                return null;
            }
            catch (TaskCanceledException)
            {
                Data = "Запрос тайм-аут";
                return null;
            }
            catch (Exception ex)
            {
                Data = "Неизвестная ошибка: " + ex.Message;
                return null;
            }
        }

        public async void Initialize()
        {
            List<Country> countries = await DeserializationAsync();

            if (countries == null || countries.Count == 0)
            {
                Data = Data ?? "Данные не найдены";
                return;
            }

            try
            {
                Country russia = countries[0];

                Data =
                    $"Страна: {russia.name?.common ?? "нет данных"}\n" +
                    $"Официальное название: {russia.name?.official ?? "нет данных"}\n" +
                    $"Столица: {capital}\n" +
                    $"Регион: {russia.region ?? "нет данных"}\n" +
                    $"Подрегион: {russia.subregion ?? "нет данных"}\n" +
                    $"Континент: {continent}\n" +
                    $"Население: {russia.population}\n" +
                    $"Площадь: {russia.area} км²\n" +
                    $"Валюта: {currencyName} ({currencySymbol})\n" +
                    $"Язык: {language}\n" +
                    $"Автодвижение: {carSide}\n" +
                    $"Член ООН: {unMember}\n";
            }
            catch (Exception ex)
            {
                Data = "Ошибка при обработке данных страны: " + ex.Message;
            }
        }
    }
}
