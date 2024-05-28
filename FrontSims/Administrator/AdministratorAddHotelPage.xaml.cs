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

namespace FrontSims.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorAddHotelPage.xaml
    /// </summary>
    public partial class AdministratorAddHotelPage : Window
    {
        public AdministratorAddHotelPage()
        {
            InitializeComponent();
        }

        private async void CreateHotel_Click(object sender, RoutedEventArgs e)
        {
            // Kreiranje novog hotela sa unetim podacima
            Hotel newHotel = new Hotel
            {
                Name = txtHotelName.Text,
                YearOfConstruction = int.Parse(txtYearOfConstruction.Text),
                NumberOfStars = int.Parse(txtNumberOfStars.Text),
                Password = txtPassword.Text,
                Owner = txtOwnerJMBG.Text,
                HotelStatus = GetHotelStatusFromComboBox()
            };

            // Slanje HTTP zahteva ka serveru za kreiranje hotela
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newHotel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:7040/api/Hotel", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Hotel successfully created!");
                }
                else
                {
                    MessageBox.Show("Failed to create hotel. Please try again.");
                }
            }
        }


        private HotelStatus GetHotelStatusFromComboBox()
        {
            ComboBoxItem selectedItem = cmbHotelStatus.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string statusText = selectedItem.Content.ToString();
                if (Enum.TryParse(statusText, out HotelStatus status))
                {
                    return status;
                }
            }
            return HotelStatus.Declined;
        }

    }
}
