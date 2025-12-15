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
                    Class1 russia = countries[0];

                    string message = $"Страна: {russia.name.common}\n" +
                                    $"Официальное название: {russia.name.official}\n" +
                                    $"Столица: {russia.capital[0]}\n" +
                                    $"Население: {russia.population}\n" +
                                    $"Площадь: {russia.area} км²\n" +
                                    $"Регион: {russia.region}";

                    box.Text = message;
                }
                else
                {
                    box.Text = "Данные о стране не найдены.";
                }
            }
            catch (Exception ex)
            {
                box.Text = $"Не удалось загрузить данные: {ex.Message}";
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
