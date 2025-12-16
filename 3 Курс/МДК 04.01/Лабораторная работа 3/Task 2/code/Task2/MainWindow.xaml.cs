using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace Task2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                List<Class1> countries = deserialization();

                if (countries != null && countries.Count > 0)
                {
                    Class1 russia = countries[0]; // Берем первую страну из массива

                    // Выводим информацию
                    string message = $"Страна: {russia.name.common}\n" +
                                    $"Официальное название: {russia.name.official}\n" +
                                    $"Столица: {russia.capital[0]}\n" +
                                    $"Население: {russia.population}\n" +
                                    $"Площадь: {russia.area} км²\n" +
                                    $"Регион: {russia.region}";

                    MessageBox.Show(message, "Информация о стране");
                }
                else
                {
                    MessageBox.Show("Данные о стране не найдены!", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private List<Class1> deserialization()
        {
            string url = "https://restcountries.com/v3.1/name/russia";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Class1>>(json);
                }
                else
                {
                    throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
                }
            }
        }
    }
}