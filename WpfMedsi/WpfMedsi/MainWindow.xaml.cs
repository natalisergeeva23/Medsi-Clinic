using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMedsi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private readonly string _apiUrl = "https://172.20.10.2:7266/api";

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;

            string token = await Authorization(login, password);
            if (token != null)
            {
                HttpClient client = new HttpClient();
                string apiUrl = "https://172.20.10.2:7266/api/Employees"; // Замените на фактический URL вашего API
                string requestUrl = $"{apiUrl}/GetRoleIdByLogin?login={login}";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string roleIdString = await response.Content.ReadAsStringAsync();

                        if (int.TryParse(roleIdString, out int roleId))
                        {
                            switch (roleId)
                            {
                                case 1:
                                    Admin admin = new Admin();
                                    admin.Show();
                                    Close();
                                    break;
                                case 2:
                                    Doctor doctor = new Doctor();
                                    doctor.Show();
                                    Close();
                                    break;
                                case 3:
                                    Pacient pacient = new Pacient();
                                    pacient.Show();
                                    Close();
                                    break;
                                case 4:
                                    Registrator registrator = new Registrator();
                                    registrator.Show();
                                    Close();
                                    break;
                            }
                        }
                        else
                        {
                            // Обработка ошибки преобразования строки в число
                            MessageBox.Show("Невозможно преобразовать RoleId в число.");
                        }
                    }
                    else
                    {
                        // Обработка ошибки, например, вывод сообщения пользователю
                        MessageBox.Show($"Ошибка: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Обработка исключения, например, вывод сообщения об ошибке
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }  
            }

        }

        private async Task<string> Authorization(string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/Employees/{login}/{password}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return null;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }
    }
}

