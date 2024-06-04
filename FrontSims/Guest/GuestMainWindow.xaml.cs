using FrontSims.CommonFunctionalities;
using FrontSims.Guest;
using Sims_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace FrontSims
{
    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window
    {
        private User user;
        public GuestMainWindow(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void OpenHotelButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new HotelListWindow();
            window.Show();
            
        }

        

            private void ApartmentReservation_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ApartmentReservationPage(user);
            window.Show();
            
        }

        private void ShowAllReservations_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ShowAllReservationsPage(user);
            window.Show();
            
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
           
        }

    }
}
