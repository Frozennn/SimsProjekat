using FrontSims.Administrator;
using FrontSims.CommonFunctionalities;
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
    /// Interaction logic for AdministratorMainWindow.xaml
    /// </summary>
    public partial class AdministratorMainWindow : Window
    {
        public AdministratorMainWindow()
        {
            InitializeComponent();
        }

        private void OpenHotelButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new HotelListWindow();
            window.Show();
            this.Close();
        }
        private void OpenRegistrationPage_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AdministratorRegisterPage();
            window.Show();
            this.Close();
        }

        private void OpenShowAllUsersPage_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AdministratorShowAllUsersPage();
            window.Show();
            this.Close();
        }

        private void OpenAddHotelPage_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AdministratorAddHotelPage();
            window.Show();
            this.Close();
        }

    }
}
