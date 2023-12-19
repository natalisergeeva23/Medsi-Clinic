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
    /// Логика взаимодействия для Registrator.xaml
    /// </summary>
    public partial class Registrator : Window
    {
        public Registrator()
        {
            InitializeComponent();
        }

        public int count;
        public async Task LoadData(int count)
        {
            HttpClient client = new HttpClient();
            switch (count)
            {
                case 2:
                    try
                    {
                        dgRecording.ItemsSource = null;

                        var columns = dgRecording.Columns;
                        columns[1].Header = "Дата записи";
                        columns[2].Header = "Время записи";
                        columns[3].Header = "Направление";

                        columns[0].Visibility = Visibility.Collapsed;

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

     

       
        private async void Button_Click_11(object sender, RoutedEventArgs e)
        {
            PutTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }




        private async void Button_Click_19(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
