using CsvHelper;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfMedsi.Models;

namespace WpfMedsi
{
    /// <summary>
    /// Логика взаимодействия для Pacient.xaml
    /// </summary>
    public partial class Pacient : Window
    {
        public Pacient()
        {
            InitializeComponent();
        }
        public int count;
        List<PatientCard> data3;
        public async Task LoadData(int count)
        {
            HttpClient client = new HttpClient();
            switch (count)
            {
              
                case 2:
                    try
                    {
                        dgPatientCard.ItemsSource = null;
                    dgRecording.ItemsSource = null;

                    var columns = dgPatientCard.Columns;
                    columns[1].Header = "Фамилия";
                    columns[2].Header = "Имя";
                    columns[3].Header = "Отчество";
                    columns[4].Header = "Серия паспорта";
                    columns[5].Header = "Номер паспорта";
                    columns[6].Header = "СНИЛС";
                    columns[7].Header = "Страховой полис";


                    columns[0].Visibility = Visibility.Collapsed;

                    var columns1 = dgRecording.Columns;
                    columns1[1].Header = "Дата записи";
                    columns1[2].Header = "Время записи";
                    columns1[3].Header = "Направление";

                    columns1[0].Visibility = Visibility.Collapsed;

                    HttpResponseMessage response3 = await client.GetAsync($"https://172.20.10.2:7266/api/PatientCards");
                    string json3 = await response3.Content.ReadAsStringAsync();
                    data3 = JsonConvert.DeserializeObject<List<PatientCard>>(json3);
                    dgPatientCard.ItemsSource = data3;
                    cmbTypeServices.ItemsSource = data3;

                    HttpResponseMessage response4 = await client.GetAsync($"https://172.20.10.2:7266/api/Recordings");
                    string json4 = await response4.Content.ReadAsStringAsync();
                    List<Recording> data4 = JsonConvert.DeserializeObject<List<Recording>>(json4);
                    dgRecording.ItemsSource = data4;
                    }
                    catch { await LoadData(2); }
                    break;
            }
        }

        private async void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            count = 1;
            await LoadData(count);
        }

        private async void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            count = 2;
            await LoadData(count);
        }

        private async void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            count = 3;
            await LoadData(count);
        }

        private async void TabItem_Loaded_3(object sender, RoutedEventArgs e)
        {
            count = 4;
            await LoadData(count);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CreateTable<PatientCard>(dgPatientCard);
            await LoadData(2);
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CreateTable<Recording>(dgRecording);
            await LoadData(2);
        }

        public async void CreateTable<T>(DataGrid name)
        {
            HttpClient client = new HttpClient();
            var selectedItems = name.SelectedItems.Cast<T>().ToList();
            if (selectedItems.Count == 1)
            {
                var data = selectedItems.FirstOrDefault();
                var properties = typeof(T).GetProperties();
                properties[0].SetValue(data, null);
                string jsonString = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                string ClassName = typeof(T).Name;
                switch (ClassName)
                {
                    case ("Synopsis"):
                        ClassName = "Synopse";
                        break;
                    case ("Status"):
                        ClassName = "Statu";
                        break;

                }
                HttpResponseMessage response = await client.PostAsync($"https://172.20.10.2:7266/api/{ClassName}s", content);
                await LoadData(count);
            }
            else
            {
                MessageBox.Show("Выберете строку!");
            }
        }

        public async void PutTable<T>(DataGrid name)
        {
            HttpClient client = new HttpClient();
            var selectedItems = name.SelectedItems.Cast<T>().ToList();
            if (selectedItems.Count == 1)
            {
                var data = selectedItems.FirstOrDefault();
                var properties = typeof(T).GetProperties();
                int ID = (int)properties[0].GetValue(data);
                string jsonString = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                string ClassName = typeof(T).Name;
                switch (ClassName)
                {
                    case ("Synopsis"):
                        ClassName = "Synopse";
                        break;
                    case ("Status"):
                        ClassName = "Statu";
                        break;

                }
                HttpResponseMessage response = await client.PutAsync($"https://172.20.10.2:7266/api/{ClassName}s/{ID}", content);
                await LoadData(count);
            }
            else
            {
                MessageBox.Show("Выберете строку!");
            }
        }

        public async Task DeleteTable<T>(DataGrid name)
        {
            HttpClient client = new HttpClient();
            var selectedItems = name.SelectedItems.Cast<T>().ToList();
            if (selectedItems.Count == 1)
            {
                var data = selectedItems.FirstOrDefault();
                var properties = typeof(T).GetProperties();
                int id = (int)properties[0].GetValue(data);
                string ClassName = typeof(T).Name;
                switch (ClassName)
                {
                    case ("Synopsis"):
                        ClassName = "Synopse";
                        break;
                    case ("Status"):
                        ClassName = "Statu";
                        break;

                }
                HttpResponseMessage response = await client.DeleteAsync($"https://172.20.10.2:7266/api/{ClassName}s/{id}");
                await LoadData(count);
            }
            else
            {
                MessageBox.Show("Выберете одну строку для удаления!");
            }
        }

        private async void Button_Click_8(object sender, RoutedEventArgs e)
        {
        }

        private async void Button_Click_10(object sender, RoutedEventArgs e)
        {
            PutTable<PatientCard>(dgPatientCard);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private async void Button_Click_11(object sender, RoutedEventArgs e)
        {
            PutTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private async void Button_Click_16(object sender, RoutedEventArgs e)
        {
        }


        private async void Button_Click_18(object sender, RoutedEventArgs e)
        {
            await DeleteTable<PatientCard>(dgPatientCard);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private async void Button_Click_19(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }


       
        private void cmbTypeServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранное значение из комбобокса
            string selectedType = (string)(cmbTypeServices.SelectedItem as PatientCard)?.CardNumberPatient;

            // Фильтруем данные в data3 в зависимости от выбранного значения
            List<PatientCard> filteredData = data3.Where(s => s.CardNumberPatient == selectedType).ToList();

            // Обновляем источник данных для DataGrid
            dgPatientCard.ItemsSource = filteredData;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Получаем текст из TextBox
            string searchText = txtSearch.Text;

            // Фильтруем данные в data7 в зависимости от введенного текста
            List<PatientCard> filteredData = data3.Where(s => s.NameDiseases.Contains(searchText)).ToList();

            // Обновляем источник данных для DataGrid
            dgPatientCard.ItemsSource = filteredData;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
