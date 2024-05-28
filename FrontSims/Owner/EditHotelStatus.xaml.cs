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
    /// Interaction logic for EditHotelStatus.xaml
    /// </summary>
    public partial class EditHotelStatus : Window
    {
        private Hotel hotel;
        public EditHotelStatus(Hotel hotel)
        {
            this.hotel = hotel;
            InitializeComponent();
        }

        private async Task UpdateHotel(string status)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Hotel");

                var updateRequest = new UpdateHotelRequest
                {
                   Status = status,
                };

                var json = JsonConvert.SerializeObject(updateRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"Hotel/{hotel.Password}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Hotel updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update hotel.");
                }
            }
        }

        private async void UpdateHotelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string status = (statusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                await UpdateHotel(status);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }
    }
}
