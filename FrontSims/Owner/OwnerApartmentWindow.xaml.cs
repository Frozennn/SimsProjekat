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
    /// Interaction logic for OwnerApartmentWindow.xaml
    /// </summary>
    public partial class OwnerApartmentWindow : Window
    {
        public OwnerApartmentWindow()
        {
            InitializeComponent();
        }

        private async void CreateHotel_Click(object sender, RoutedEventArgs e)
        {
            // Kreiranje novog hotela sa unetim podacima
           Apartment newApartment = new Apartment
            {
                Name = txtApartmentName.Text,
                Description = txtDescription.Text,
                NumberOfRooms = int.Parse(txtNumberOfRooms.Text),
                MaxGuestNumber = int.Parse(txtMaxGuestNumber.Text),
                OwnerJMBG = txtOwnerJMBG.Text,
                HotelPassword = txtHotelPassword.Text,
            };

            // Slanje HTTP zahteva ka serveru za kreiranje hotela
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newApartment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:7040/api/Apartment", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Apartment successfully added!");
                }
                else
                {
                    MessageBox.Show("Failed to add Apartment. Please try again.");
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
        }
    }
}
