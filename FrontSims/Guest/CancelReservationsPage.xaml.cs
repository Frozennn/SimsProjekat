using Newtonsoft.Json;
using Sims_Projekat.Model;
using SIMS_PROJEKAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
    /// Interaction logic for CancelReservationsPage.xaml
    /// </summary>
    public partial class CancelReservationsPage : Window
    {
        private User user;
        public CancelReservationsPage(User user)
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
                client.BaseAddress = new Uri($"https://localhost:7040/api/Reservation/NotCancelledByGuestJMBG?JMBG={user.JMBG}");

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

        private async Task<IEnumerable<Reservation>> CancelReservation(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:7040/api/Reservation/Cancel?id={id}");

                var content = new StringContent(user.JMBG, Encoding.UTF8, "application/json");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var reservationsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(reservationsJson);

                }
                else
                {
                    throw new Exception("Failed to delete reservation");

                }
            }
        }

        private async void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = (DataGridRow)reservationsDataGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);

            // Dobijanje korisnika iz reda
            Reservation reservation = (Reservation)row.Item;
            try
            {

                IEnumerable<Reservation> reservations = await CancelReservation(reservation.Id);
                LoadReservations();
               // reservationsDataGrid.ItemsSource = reservations;
               // reservationsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
