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

namespace FrontSims.Guest
{
    /// <summary>
    /// Interaction logic for ApartmentReservationPage.xaml
    /// </summary>
    public partial class ApartmentReservationPage : Window
    {
        private User user;
        public ApartmentReservationPage(User user)
        {
            this.user = user;
            InitializeComponent();
            LoadApartments();
        }

        private async void LoadApartments()
        {
            try
            {
                IEnumerable<Apartment> apartments = await GetAllApartments();
                apartmentsDataGrid.ItemsSource = apartments;
                apartmentsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task<IEnumerable<Apartment>> GetAllApartments()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Apartment");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var apartmentsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Apartment>>(apartmentsJson);

                }
                else
                {
                    throw new Exception("Failed to get apartments");

                }
            }
        }

        private async void ReserveApartment_Click(object sender, RoutedEventArgs e)
        {
            // Dobijanje reda u kojem je kliknuto dugme
            DataGridRow row = (DataGridRow)apartmentsDataGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            Apartment apartment = (Apartment)row.Item;

            Window window = new ScheduleReservationPage(apartment,user);
            window.Show();
            this.Close();
           
        }
      

    }
}
