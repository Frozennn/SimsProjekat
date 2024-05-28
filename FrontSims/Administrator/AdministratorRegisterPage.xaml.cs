using Newtonsoft.Json;
using Sims_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace FrontSims.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorRegisterPage.xaml
    /// </summary>
    public partial class AdministratorRegisterPage : Window
    {
        public AdministratorRegisterPage()
        {
            InitializeComponent();
        }

       

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                JMBG = txtJMBG.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                UserType = cmbUserType.SelectedIndex == 0 ? UserType.Guest : UserType.Owner, 
                IsBlocked = false
            };

            try
            {
                User registeredUser = await RegisterUserAsync(user);
                MessageBox.Show("User registered successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering user: {ex.Message}");
            }
        }

        private async Task<User> RegisterUserAsync(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/User");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsonUser = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:7040/api/User", content);

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var registeredUser = JsonConvert.DeserializeObject<User>(userJson);
                    return registeredUser;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
    }
}

