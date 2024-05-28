using FrontSims.CommonFunctionalities;
using Newtonsoft.Json;
using Sims_Projekat.Controller;
using Sims_Projekat.Model;
using Sims_Projekat.Service;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontSims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int failedLoginAttempts = 0;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async Task<bool> Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/User/login");
                var data = new
                {
                    Email = username,
                    Password = password
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content);

                return response.IsSuccessStatusCode ? true : false;
            }
        }

        private User GetUserByEmail(string email)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/User");


                var response = client.GetAsync($"User/email/{email}").Result;
                

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    var user = JsonConvert.DeserializeObject<User>(userJson);

                    return user;
                }
                else
                {
                    MessageBox.Show("User not found");
                    return null; 
                }
            }
        }

        private async void Login_Click(object sender,  RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            
            bool isAuthenticated = await Login(username, password);
            var user = GetUserByEmail(username);
            Window window = null;

            if (isAuthenticated)
            {
            switch(user.UserType)
                {
                    case UserType.Administrator:
                      window = new AdministratorMainWindow();
                        break;
                    case UserType.Guest:
                        window = new GuestMainWindow(user);
                        break;
                    case UserType.Owner: 
                       window = new OwnerMainWindow(user);
                        
                        break;
                    default:
                        MessageBox.Show("Unknown User Type");
                        break;
                }

                window.Show();
                this.Close();
                
            }
            else
            {
                failedLoginAttempts++;
                if(failedLoginAttempts >= 3)
                {
                    MessageBox.Show("Too many failed login attempts. The application will now close.");
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
                
            }

        }
    }
}
