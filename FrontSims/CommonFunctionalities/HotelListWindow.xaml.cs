using Newtonsoft.Json;
using Sims_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontSims.CommonFunctionalities
{
    /// <summary>
    /// Interaction logic for HotelListWindow.xaml
    /// </summary>
    public partial class HotelListWindow : Window
    {
        public HotelListWindow()
        {
            InitializeComponent();
            LoadHotels();

        }

        private async void LoadHotels()
        {
            try
            {
                IEnumerable<Hotel> hotels = await GetAllHotels();
                hotelsDataGrid.ItemsSource = hotels;
                hotelsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Hotel/api/Hotel/approvedHotels");

                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelsJson);

                }
                else
                {
                    throw new Exception("Failed to get hotels");

                }
            }
        }


        private async Task<IEnumerable<Hotel>> SearchHotels(string searchTerm,string searchParameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Hotel");

                var response = await client.GetAsync($"Hotel/search?searchTerm={searchTerm}&searchParameter={searchParameter}");
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelsJson);

                }
                else
                {
                    throw new Exception("Failed to get hotels");

                }
            }
        }

        private async Task<IEnumerable<Hotel>> SearchHotelsByApartments(string searchTerm, string searchParameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Hotel");

                var encodedSearchTerm = HttpUtility.UrlEncode(searchTerm);

                var response = await client.GetAsync($"Hotel/searchApartments?searchTerm={encodedSearchTerm}&searchParameter={searchParameter}");
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelsJson);

                }
                else
                {
                    throw new Exception("Failed to get hotels");

                }
            }
        }

        private async Task<IEnumerable<Hotel>> GetSortedHotelsByStars()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Hotel");

                var response = await client.GetAsync("Hotel/sortByStars");
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelsJson);
                }
                else
                {
                    throw new Exception("Failed to get sorted hotels by stars");
                }
            }

        }

        private async void LoadSortedHotelsByStars()
        {
            try
            {
                IEnumerable<Hotel> hotels = await GetSortedHotelsByStars();
                hotelsDataGrid.ItemsSource = hotels;
                hotelsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private async Task<IEnumerable<Hotel>> GetSortedHotelsByName()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/Hotel");

                var response = await client.GetAsync("Hotel/sortByName");
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelsJson);
                }
                else
                {
                    throw new Exception("Failed to get sorted hotels by name");
                }
            }

        }

        private async void LoadSortedHotelsByName()
        {
            try
            {
                IEnumerable<Hotel> hotels = await GetSortedHotelsByName();
                hotelsDataGrid.ItemsSource = hotels;
                hotelsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void SortByStarsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSortedHotelsByStars();
        }

        private void SortByNameButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSortedHotelsByName();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchTerm = txtSearchTerm.Text;
                string searchParameter = ((ComboBoxItem)cmbUserTypeFilter.SelectedItem).Content.ToString();
                if(searchParameter != "Apartments")
                {
                    IEnumerable<Hotel> hotels = await SearchHotels(searchTerm, searchParameter);

                    // Postavite izlistane hotele u DataGrid
                    hotelsDataGrid.ItemsSource = hotels;
                    hotelsDataGrid.Items.Refresh();
                }
                else
                {

                    searchParameter = ((ComboBoxItem)cmbApartmentSearchOption.SelectedItem).Tag.ToString();
                    IEnumerable<Hotel> hotels = await SearchHotelsByApartments(searchTerm, searchParameter);

                    // Postavite izlistane hotele u DataGrid
                    hotelsDataGrid.ItemsSource = hotels;
                    hotelsDataGrid.Items.Refresh();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void cmbUserTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbUserTypeFilter.SelectedItem != null && cmbApartmentSearchOption != null)
            {
                string selectedTag = ((ComboBoxItem)cmbUserTypeFilter.SelectedItem).Tag.ToString();
                if (selectedTag == "Apartments")
                {
                    cmbApartmentSearchOption.Visibility = Visibility.Visible;
                }
                else
                {
                    cmbApartmentSearchOption.Visibility = Visibility.Collapsed;
                }
            }
        }


    }

  
}
