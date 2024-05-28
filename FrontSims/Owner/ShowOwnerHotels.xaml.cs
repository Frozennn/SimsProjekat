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

namespace FrontSims.Owner
{
    /// <summary>
    /// Interaction logic for ShowOwnerHotels.xaml
    /// </summary>
    public partial class ShowOwnerHotels : Window
    {
        private User user;
        public ShowOwnerHotels(User user)
        {
            this.user = user;
            InitializeComponent();
            LoadHotels();
        }

        private async void LoadHotels()
        {
            try
            {
                IEnumerable<Hotel> hotels = await GetAllHotels();
                hotelsDataGrid.ItemsSource = hotels;
                hotelsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:7040/api/Hotel/GetAllOwnersHotels?jmbg={user.JMBG}");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelsJson);

                }
                else
                {
                    throw new Exception("Failed to get hotels");

                }
            }
        }

        private async void SeeReservations_Click(object sender, RoutedEventArgs e)
        {
            // Dobijanje reda u kojem je kliknuto dugme
            DataGridRow row = (DataGridRow)hotelsDataGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);

            // Dobijanje hotela iz reda
            Hotel hotel = (Hotel)row.Item;

            Window window = new ShowReservationsForHotel(hotel);
            window.Show();
            this.Close();
        }
    }
}
