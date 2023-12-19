using CsvHelper;
using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Net.NetworkInformation;
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
using Position = WpfMedsi.Models.Position;

namespace WpfMedsi
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
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
                case 1:
                    try
                    {
                        dgEmployee.ItemsSource = null;
                        dgPatient.ItemsSource = null;

                    HttpResponseMessage response = await client.GetAsync($"https://172.20.10.2:7266/api/Employees");
                    string json1 = await response.Content.ReadAsStringAsync();
                    List<Employee> data = JsonConvert.DeserializeObject<List<Employee>>(json1);

                        dgEmployee.ItemsSource = data;

                        var columns = dgEmployee.Columns;

                        columns[0].Visibility = Visibility.Collapsed;
                        columns[1].Header = "Фамилия";
                        columns[2].Header = "Имя";
                        columns[3].Header = "Отчество";
                        columns[4].Header = "Логин";
                        columns[5].Header = "Пароль";
                        columns[6].Visibility = Visibility.Collapsed;
                        columns[7].Visibility = Visibility.Collapsed;
                        columns[8].Visibility = Visibility.Collapsed;
                        columns[9].Visibility = Visibility.Collapsed;
                        columns[10].Visibility = Visibility.Collapsed;


                       


                        HttpResponseMessage response2 = await client.GetAsync($"https://172.20.10.2:7266/api/Patients");
                    string json2 = await response2.Content.ReadAsStringAsync();
                    List<Patient> data2 = JsonConvert.DeserializeObject<List<Patient>>(json2);

                        dgPatient.ItemsSource = data2;

                        var columns1 = dgPatient.Columns;


                        columns1[0].Visibility = Visibility.Collapsed;
                        columns1[1].Header = "Фамилия";
                        columns1[2].Header = "Имя";
                        columns1[3].Header = "Отчество";
                        columns1[4].Header = "Серия паспорта";
                        columns1[5].Header = "Номер паспорта";
                        columns1[6].Header = "СНИЛС";
                        columns1[7].Header = "Полис";
                        columns1[8].Header = "Логин";
                        columns1[9].Header = "Пароль";
                    }
                    catch { /*await LoadData(1);*/ }
                    break;
                case 2:
                    try
                    {
                        dgRecording.ItemsSource = null;

                        HttpResponseMessage response4 = await client.GetAsync($"https://172.20.10.2:7266/api/Recordings");
                        string json4 = await response4.Content.ReadAsStringAsync();
                        List<Recording> data4 = JsonConvert.DeserializeObject<List<Recording>>(json4);

                        dgRecording.ItemsSource = data4;

                        var columns2 = dgRecording.Columns;

                        columns2[0].Visibility = Visibility.Collapsed;
                        columns2[1].Header = "Дата записи";
                        columns2[2].Header = "Время записи";
                        columns2[3].Header = "Направление";
                        columns2[4].Visibility = Visibility.Collapsed;
                        columns2[5].Visibility = Visibility.Collapsed;
                        columns2[6].Visibility = Visibility.Collapsed;
                    }
                    catch { /*await LoadData(2);*/}
                    break;
                case 3:
                    try
                    {
                        dgPosition.ItemsSource = null;
                        dgRole.ItemsSource = null;

                        

                        HttpResponseMessage response5 = await client.GetAsync($"https://172.20.10.2:7266/api/Positions");
                        string json5 = await response5.Content.ReadAsStringAsync();
                        List<Position> data5 = JsonConvert.DeserializeObject<List<Position>>(json5);

                        dgPosition.ItemsSource = data5;

                        var columns3 = dgPosition.Columns;
                        columns3[0].Visibility = Visibility.Collapsed;
                        columns3[1].Header = "Должность";


                        HttpResponseMessage response6 = await client.GetAsync($"https://172.20.10.2:7266/api/Roles");
                        string json6 = await response6.Content.ReadAsStringAsync();
                        List<Role> data6 = JsonConvert.DeserializeObject<List<Role>>(json6);

                        dgRole.ItemsSource = data6;
                        var columns4 = dgRole.Columns;
                        columns4[0].Visibility = Visibility.Collapsed;
                        columns4[1].Header = "Роль";
                        columns4[2].Visibility = Visibility.Collapsed;
                    }
                    catch {  /*await LoadData(3);*/ }
                    break;
                case 4:
                    try
                    {
                        dgStatus.ItemsSource = null;
                        dgSynopsis.ItemsSource = null;


                        HttpResponseMessage response7 = await client.GetAsync($"https://172.20.10.2:7266/api/Status");
                        string json7 = await response7.Content.ReadAsStringAsync();
                        List<Status> data7 = JsonConvert.DeserializeObject<List<Status>>(json7);


                        dgStatus.ItemsSource = data7;


                        var columns5 = dgStatus.Columns;
                        columns5[0].Visibility = Visibility.Collapsed;
                        columns5[1].Header = "Статус";
                        columns5[0].Visibility = Visibility.Collapsed;

                        HttpResponseMessage response8 = await client.GetAsync($"https://172.20.10.2:7266/api/Synopses");
                        string json8 = await response8.Content.ReadAsStringAsync();
                        List<Synopsis> data8 = JsonConvert.DeserializeObject<List<Synopsis>>(json8);


                        dgSynopsis.ItemsSource = data8;

                        var columns6 = dgSynopsis.Columns;
                        columns6[0].Visibility = Visibility.Collapsed;
                        columns6[1].Header = "Справка";
                        columns6[2].Visibility = Visibility.Collapsed;
                    }
                    catch {  /*await LoadData(4);*/ }
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
            CreateTable<Employee>(dgEmployee);
            await Task.Delay(1000);
            await LoadData(1);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateTable<Patient>(dgPatient);
            await Task.Delay(1000);
            await LoadData(1);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CreateTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CreateTable<Position>(dgPosition);
            await Task.Delay(1000);
            await LoadData(3);
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CreateTable<Role>(dgRole);
            await Task.Delay(1000);
            await LoadData(3);
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            CreateTable<Status>(dgStatus);
            await Task.Delay(1000);
            await LoadData(4);
        }

        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            CreateTable<Synopsis>(dgSynopsis);
            await Task.Delay(1000);
            await LoadData(4);
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
            PutTable<Employee>(dgEmployee);
            await Task.Delay(1000);
            await LoadData(1);
        }

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            PutTable<Patient>(dgPatient);
            await Task.Delay(1000);
            await LoadData(1);
        }

        private async void Button_Click_10(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click_11(object sender, RoutedEventArgs e)
        {
            PutTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private async void Button_Click_12(object sender, RoutedEventArgs e)
        {
            PutTable<Position>(dgPosition);
            await Task.Delay(1000);
            await LoadData(3);
        }

        private async void Button_Click_13(object sender, RoutedEventArgs e)
        {
            PutTable<Role>(dgRole);
            await Task.Delay(1000);
            await LoadData(3);
        }

        private async void Button_Click_14(object sender, RoutedEventArgs e)
        {
            PutTable<Status>(dgStatus);
            await Task.Delay(1000);
            await LoadData(4);
        }

        private async void Button_Click_15(object sender, RoutedEventArgs e)
        {
            PutTable<Synopsis>(dgSynopsis);
            await Task.Delay(1000);
            await LoadData(4);
        }

        private async void Button_Click_16(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Employee>(dgEmployee);
            await Task.Delay(1000);
            await LoadData(1);
        }

        private async void Button_Click_17(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Patient>(dgPatient);
            await Task.Delay(1000);
            await LoadData(1);
        }

        private async void Button_Click_18(object sender, RoutedEventArgs e)
        {
        }

        private async void Button_Click_19(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Recording>(dgRecording);
            await Task.Delay(1000);
            await LoadData(2);
        }

        private async void Button_Click_20(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Position>(dgPosition);
            await Task.Delay(1000);
            await LoadData(3);
        }

        private async void Button_Click_21(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Role>(dgRole);
            await Task.Delay(1000);
            await LoadData(3);
        }

        private async void Button_Click_22(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Status>(dgStatus);
            await Task.Delay(1000);
            await LoadData(4);
        }

        private async void Button_Click_23(object sender, RoutedEventArgs e)
        {
            await DeleteTable<Synopsis>(dgSynopsis);
            await Task.Delay(1000);
            await LoadData(4);
        }

        private void ExportToSql_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-OD7QUTU\\MYSERVIS;Initial Catalog=Medsi;User ID=sa;Password=23082004"))
            {
                connection.Open();

                List<Position> data = dgPosition.ItemsSource.Cast<Position>().ToList();

                foreach (var item in data)
                {
                    string insertQuery = $"INSERT INTO Position (Name_Position) VALUES ('{item.NamePosition}')";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Export to SQL (Position) completed.");
        }

        private void ExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            List<Position> data = dgPosition.ItemsSource.Cast<Position>().ToList();

            using (var writer = new StreamWriter("Position.csv", false, Encoding.UTF8)) // Используем UTF-8
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
            }

            MessageBox.Show("Export to CSV (Position) completed.");

        }

        private void ImportFromSql_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-OD7QUTU\\MYSERVIS;Initial Catalog=Medsi;User ID=sa;Password=23082004"))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Position";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    List<Position> importedData = new List<Position>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Position position = new Position
                            {
                                IdPosition = Convert.ToInt32(reader["ID_Position"]),
                                NamePosition = reader["Name_Position"].ToString()
                            };

                            importedData.Add(position);
                        }
                    }

                    dgPosition.ItemsSource = importedData;
                }
            }

            MessageBox.Show("Import from SQL (Position) completed.");
        }

        private void ImportFromCsv_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                List<Position> importedData = new List<Position>();

                foreach (var line in lines.Skip(1)) // Skip header
                {
                    var values = line.Split(',');
                    if (values.Length == 2)
                    {
                        Position booking = new Position
                        {
                            IdPosition = int.Parse(values[0]),
                            NamePosition = values[1]
                        };

                        importedData.Add(booking);
                    }
                }

                dgPosition.ItemsSource = importedData;
                MessageBox.Show("Import from CSV completed.");
            }
        }

     
        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            ClearPlot(PlotGraph);
            List<Synopsis> data = GetDataFromGrid<Synopsis>(dgSynopsis); // получение данных из базы данных
            PlotData<Synopsis>(data, PlotGraph); // генерация графика
        }

        public List<T> GetDataFromGrid<T>(DataGrid name)
        {
            List<T> itemsSourceList = new List<T>();

            if (name.ItemsSource is IEnumerable<T> itemsSource)
            {
                itemsSourceList = itemsSource.ToList();
            }
            else if (name.ItemsSource is IEnumerable itemsSourceNonGeneric)
            {
                foreach (var item in itemsSourceNonGeneric)
                {
                    if (item is T tItem)
                    {
                        itemsSourceList.Add(tItem);
                    }
                }
            }
            return itemsSourceList;
        }

        public void PlotData<T>(List<T> data, ScottPlot.WpfPlot plotControl)
        {
            double[] xValues;
            double[] yValues;

            // выбираем данные для графика из List<T> для таблицы 1
            // например, первый столбец - это x, второй столбец - это y
            xValues = data.Select(item => Convert.ToDouble(item.GetType().GetProperty("IdSynopsis").GetValue(item))).ToArray();
            yValues = data.Select(item => Convert.ToDouble(item.GetType().GetProperty("PatientCardId").GetValue(item))).ToArray();
            plotControl.Plot.XLabel("ID");
            plotControl.Plot.YLabel("Пациенты");
            plotControl.Plot.Title("График данных");


            // добавляем точки на существующий объект ScottPlot.WpfPlot
            plotControl.Plot.AddScatter(xValues, yValues);
            // обновляем отображение графика
            plotControl.Render();
        }

        public void ClearPlot(ScottPlot.WpfPlot plotControl)
        {
            plotControl.Reset();
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            string databaseName = "Medsi"; // Замените на имя вашей базы данных

            try
            {
                // Диалоговое окно выбора файла для сохранения резервной копии
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Backup files (*.bak)|*.bak";
                saveFileDialog.FileName = $"{databaseName}_Backup.bak";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string backupPath = saveFileDialog.FileName;

                    using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-OD7QUTU\\MYSERVIS;Initial Catalog=Medsi;User ID=sa;Password=23082004"))
                    {
                        connection.Open();

                        // Создание резервной копии базы данных
                        string backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupPath}'";

                        using (SqlCommand command = new SqlCommand(backupQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Backup created successfully. Path: {backupPath}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating backup: {ex.Message}");
            }
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
