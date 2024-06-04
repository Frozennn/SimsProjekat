using Newtonsoft.Json;
using Sims_Projekat.Model;
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
using System.Windows.Shapes;

namespace FrontSims.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorShowAllUsersPage.xaml
    /// </summary>
    public partial class AdministratorShowAllUsersPage : Window
    {
        public AdministratorShowAllUsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            try
            {
                IEnumerable<User> users = await GetAllUsers();
                usersDataGrid.ItemsSource = users;
                usersDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async Task<IEnumerable<User>> GetAllUsers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/User");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<User>>(usersJson);

                }
                else
                {
                    throw new Exception("Failed to get users");

                }
            }
        }

        private async Task<IEnumerable<User>> GetSortedUsersByFirstName()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/User");

                var response = await client.GetAsync("User/sortByFirstName");
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<User>>(usersJson);
                }
                else
                {
                    throw new Exception("Failed to get sorted hotels by stars");
                }
            }

        }

        private async void LoadSortedUsersByFirstName()
        {
            try
            {
                IEnumerable<User> users = await GetSortedUsersByFirstName();
                usersDataGrid.ItemsSource = users;
                usersDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private async Task<IEnumerable<User>> GetSortedUsersByLastName()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/User");

                var response = await client.GetAsync("User/sortByLastName");
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<User>>(usersJson);
                }
                else
                {
                    throw new Exception("Failed to get sorted hotels by name");
                }
            }

        }

        private async void LoadSortedUsersByLastName()
        {
            try
            {
                IEnumerable<User> users = await GetSortedUsersByLastName();
                usersDataGrid.ItemsSource = users;
                usersDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task<IEnumerable<User>> GetUsersByTypeAsync(string userType)
        {
            string apiUrl = $"https://localhost:7040/api/User/ByType?userType={userType}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(usersJson);
                    return users;
                }
                else
                {
                    MessageBox.Show("Error fetching users by type.");
                    return null;
                }
            }
        }

        private void SortByFirstNameButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSortedUsersByFirstName();
        }

        private void SortByLastNameButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSortedUsersByLastName();
        }

        private async void Filter_Click(object sender, RoutedEventArgs e)
        {
            string userType = (cmbUserTypeFilter.SelectedItem as ComboBoxItem)?.Tag.ToString();
            IEnumerable<User> users = await GetUsersByTypeAsync(userType);
            if (users != null)
            {
                usersDataGrid.ItemsSource = users;
                usersDataGrid.Items.Refresh();

            }
        }


        private async void BlockUnblock_Click(object sender, RoutedEventArgs e)
        {
            // Dobijanje reda u kojem je kliknuto dugme
            DataGridRow row = (DataGridRow)usersDataGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);

            // Dobijanje korisnika iz reda
            User user = (User)row.Item;

            // Pozivanje odgovarajuće akcije u kontroleru za blokiranje/deblokiranje korisnika
            string apiUrl = $"https://localhost:7040/api/User/{(user.IsBlocked ? "Unblock" : "Block")}?JMBG={user.JMBG}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    // Osvežavanje podataka nakon uspešnog blokiranja/deblokiranja korisnika
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Error blocking/unblocking user.");
                }
            }
        }


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AdministratorMainWindow();
            window.Show();
       
        }


    }
}

