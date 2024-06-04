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
using System.Windows.Media.Animation;
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
          //  CheckGuestStatus();
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

        /*private async Task<bool> IsGuestLogged(Reservation reservation)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7040/api/User/{reservation.GuestJMBG}");
                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(userJson);
                    return user.UserType == UserType.Guest;
                }
                else
                {
                    return false;
                }
            }
        }

        private async void CheckGuestStatus()
        {
            try
            {
                bool isGuest = await IsGuestLogged(reservation);

                if (isGuest)
                {
                    // Onemogući opciju "Approved" ako je korisnik GUEST
                    foreach (ComboBoxItem item in statusComboBox.Items)
                    {
                        if (item.Content.ToString() == "Approved")
                        {
                            item.IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("greska",ex);
            }
        }
        */
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
        }

       

    }
}
