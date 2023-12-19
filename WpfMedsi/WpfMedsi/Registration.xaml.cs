using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfMedsi
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            
                // Задаем адрес вашего API
                string apiUrl = "https://172.20.10.2:7266/api/Employees";

                // Создаем объект, который будет отправлен в теле запроса
                var employeeData = new
                {
                    firstNameEmp = FirstName.Text,
                    secondNameEmp = LastName.Text,
                    middleNameEmp = MiddleName.Text,
                    loginEmp = Login.Text,
                    passwordEmp = Password.Text,
                    positionId = 1, // Замените на реальный идентификатор должности
                    roleId = 3, // Замените на реальный идентификатор роли
                    salt = "string"
                };

                // Сериализуем объект в формат JSON
                var employeeJson = JsonConvert.SerializeObject(employeeData);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (senders, cert, chain, sslPolicyErrors) => true;
            // Отправляем POST-запрос к API
            var response = await client.PostAsync(apiUrl, new StringContent(employeeJson, Encoding.UTF8, "application/json"));

                // Проверяем успешность запроса
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Пользователь зарегистрирован успешно!");
                }
                else
                {
                    MessageBox.Show($"Ошибка при регистрации пользователя. Код ошибки: {response.StatusCode}");
                }
            
        }
    }
}
