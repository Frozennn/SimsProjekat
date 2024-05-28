using Newtonsoft.Json;
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
    /// Interaction logic for EditReservation.xaml
    /// </summary>
    public partial class EditReservation : Window
    {
        private Reservation reservation;
        public EditReservation(Reservation reservation)
        {
            this.reservation = reservation;
            InitializeComponent();
        }

        private async Task UpdateReservation(string status, string description)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Reservation");

                var updateRequest = new UpdateReservationRequest
                {
                    Status = status,
                    Description = description
                };

                var json = JsonConvert.SerializeObject(updateRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"Reservation/{reservation.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reservation updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update reservation.");
                }
            }
        }

        private async void UpdateReservationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Vaš kod koji pravi zahtev serveru
                string status = (statusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string description = descriptionTextBox.Text;

                await UpdateReservation(status, description);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            
        }

    }
}
