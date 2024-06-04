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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrontSims.CommonFunctionalities;
using FrontSims.Owner;
using Sims_Projekat.Model;

namespace FrontSims
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        private User user;
        public OwnerMainWindow(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void OpenHotelButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new HotelListWindow();
            window.Show();
        }

        

         private void AddApartment_Click(object sender, RoutedEventArgs e)
        {
            Window window = new OwnerApartmentWindow();
            window.Show();
        }

        private void ShowReservations_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ShowReservationsWindow();
            window.Show();
        }

        private void OpenOwnerHotelButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ShowOwnerHotels(user);
            window.Show();
        }

        private void ApproveHotels_Click(object sender, RoutedEventArgs e)
        {
            Window window = new OwnerApproveAndDeclineHotels(user);
            window.Show();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
        }
        


    }
}
