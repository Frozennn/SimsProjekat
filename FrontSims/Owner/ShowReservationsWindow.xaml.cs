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

namespace FrontSims.Owner
{
    /// <summary>
    /// Interaction logic for ShowReservationsWindow.xaml
    /// </summary>
    public partial class ShowReservationsWindow : Window
    {
        public ShowReservationsWindow()
        {
            InitializeComponent();
            LoadReservations();
        }

        private async void LoadReservations()
        {
            try
            {
                IEnumerable<Reservation> reservations = await GetAllReservations();
                reservationsDataGrid.ItemsSource = reservations;
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
                client.BaseAddress = new Uri("https://localhost:7040/api/Reservation/NotCancelled");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var reservationsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(reservationsJson);

                }
                else
                {
                    throw new Exception("Failed to get reservations");

                }
            }
        }

        private async void Filter_Click(object sender, RoutedEventArgs e)
        {
            string reservationStatus = (cmbReservationsStatusFilter.SelectedItem as ComboBoxItem)?.Tag.ToString();
            IEnumerable<Reservation> reservations = await SortReservationsByStatusForOwner(reservationStatus);
            if (reservations != null)
            {
                reservationsDataGrid.ItemsSource = reservations;
                reservationsDataGrid.Items.Refresh();

            }
        }

        private async Task<IEnumerable<Reservation>> SortReservationsByStatusForOwner(string status)
        {
            string apiUrl = $"https://localhost:7040/api/Reservation/SortByStatusForOwner?status={status}";
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
                    MessageBox.Show("Error fetching reservations by type.");
                    return null;
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
