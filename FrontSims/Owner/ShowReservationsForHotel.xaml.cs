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
    /// Interaction logic for ShowReservationsForHotel.xaml
    /// </summary>
    public partial class ShowReservationsForHotel : Window
    {
        private Hotel hotel;
        public ShowReservationsForHotel(Hotel hotel)
        {
            this.hotel = hotel;
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
                client.BaseAddress = new Uri($"https://localhost:7040/api/Reservation/HotelPassword?password={hotel.Password}");

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

        private async void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = (DataGridRow)reservationsDataGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);

            // Dobijanje hotela iz reda
            Reservation reservation = (Reservation)row.Item;
            Window window = new EditReservation(reservation);
            window.Show();
            this.Close();
        }

        
    }
}
