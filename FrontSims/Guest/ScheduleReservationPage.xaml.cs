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

namespace FrontSims.Guest
{
    /// <summary>
    /// Interaction logic for ScheduleReservationPage.xaml
    /// </summary>
    public partial class ScheduleReservationPage : Window
    {
        private Apartment apartment;
        private User user;
        public ScheduleReservationPage(Apartment apartment,User user)
        {
            InitializeComponent();
            this.apartment = apartment;
            this.user = user;
        }


        private async void ConfirmReservation_Click(object sender, RoutedEventArgs e)
        {
            Reservation NewReservation = new Reservation()
            {
                ApartmentName = apartment.Name,
                ReservationDate = datePicker.SelectedDate ?? DateTime.UtcNow,
                Status = ReservationStatus.OnHold,
                GuestJMBG = user.JMBG,
            };

            using (HttpClient client = new HttpClient())
            {
                NewReservation.ReservationDate = DateTime.SpecifyKind(NewReservation.ReservationDate, DateTimeKind.Utc);
                var json = JsonConvert.SerializeObject(NewReservation);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:7040/api/Reservation", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reservation successfully sent for Approval!");
                }
                else
                {
                    MessageBox.Show("Failed to send Reservation. Please try again.");
                }
            }

        }
        
    }
}
