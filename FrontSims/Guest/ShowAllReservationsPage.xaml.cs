using Newtonsoft.Json;
using Sims_Projekat.Model;
using SIMS_PROJEKAT.Model;
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

namespace FrontSims.Guest
{
    /// <summary>
    /// Interaction logic for ShowAllReservationsPage.xaml
    /// </summary>
    public partial class ShowAllReservationsPage : Window
    {
        private User user;
        public ShowAllReservationsPage(User user)
        {
            this.user = user;
            InitializeComponent();
            LoadReservations();
            
        }

        private async void LoadReservations()
        {
            try
            {
                IEnumerable<Reservation> reservations = await GetAllReservations();
                reservationsDataGrid.ItemsSource =reservations;
                reservationsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:7040/api/Reservation/GuestJMBG?JMBG={user.JMBG}");

                var content = new StringContent(user.JMBG, Encoding.UTF8, "application/json");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var reservationsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(reservationsJson);

                }
                else
                {
                    throw new Exception("Failed to get users");

                }
            }
        }

        private async void Filter_Click(object sender, RoutedEventArgs e)
        {
            string reservationStatus = (cmbReservationsStatusFilter.SelectedItem as ComboBoxItem)?.Tag.ToString();
            IEnumerable<Reservation> reservations = await SortReservationsByStatus(user.JMBG, reservationStatus);
            if (reservations != null)
            {
                reservationsDataGrid.ItemsSource = reservations;
                reservationsDataGrid.Items.Refresh();

            }
        }

        private void CancelReservationsPage_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CancelReservationsPage(user);
            window.Show();
            this.Close();
        }
        

        private async Task<IEnumerable<Reservation>> SortReservationsByStatus(string jmbg,string status)
        {
            string apiUrl = $"https://localhost:7040/api/Reservation/SortByStatus?JMBG={jmbg}&status={status}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var reservationsJson = await response.Content.ReadAsStringAsync();
                    var reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationsJson);
                    return reservations;
                }
                else
                {
                    MessageBox.Show("Error fetching users by type.");
                    return null;
                }
            }
        }
    }
}
