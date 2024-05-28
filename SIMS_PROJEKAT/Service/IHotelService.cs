using Sims_Projekat.Model;
using Sims_Projekat.Repository;
using SIMS_PROJEKAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Service
{
    public interface IHotelService
    {
        List<Hotel> GetAllHotels();
        List<Hotel> GetAllOwnersHotels(string jmbg);
        List<Hotel> GetAllApprovedHotels();
        void CreateHotel(Hotel hotel);
        List<Hotel> SortHotelsByName();
        List<Hotel> SortHotelsByStars();
        List<Hotel> SearchHotels(string searchTerm, string searchParameter);
        List<Hotel> SearchHotelsByApartments(string searchTerm, string searchParameter);
        void UpdateHotel(string hotelPassword, string status);
    }

    public class HotelService : IHotelService
    {
        private readonly IHotelRepository Repository;

       public HotelService(IHotelRepository repository)
        {
            Repository = repository;
        }

        public List<Hotel> GetAllHotels()
        {
            return Repository.GetAllHotels();
        }

        public List<Hotel> GetAllOwnersHotels(string jmbg)
        {
            List<Hotel> Hotels = Repository.GetAllHotels();
            return Hotels.Where(h => h.Owner == jmbg && h.HotelStatus != HotelStatus.Declined).ToList(); 
        }

        public void CreateHotel(Hotel hotel)
        {
            Repository.CreateHotel(hotel);
        }

        public List<Hotel> GetAllApprovedHotels()
        {
           return  Repository.GetAllApprovedHotels().ToList();
        }

        public List<Hotel> SortHotelsByName()
        {
            List<Hotel> Hotels = Repository.GetAllHotels();
            return Hotels.OrderBy(h => h.Name).ToList();
        }

        public List<Hotel> SortHotelsByStars()
        {
            List<Hotel> Hotels = Repository.GetAllHotels();
            return Hotels.OrderBy(h => h.NumberOfStars).ToList();
        }

        public List<Hotel> SearchHotels(string searchTerm, string searchParameter)
        {
            return Repository.SearchHotels(searchTerm, searchParameter);
        }

        public void UpdateHotel(string hotelPassword, string status)
        {
            Hotel Hotel = Repository.GetHotelByPassword(hotelPassword);
            if (Hotel == null) throw new Exception("Hotel not found");

            if (Enum.TryParse(status, out HotelStatus statusEnum))
            {
                Hotel.HotelStatus = statusEnum;
                Repository.UpdateHotel(Hotel);
            }
        }

        public List<Hotel> SearchHotelsByApartments(string searchTerm, string searchParameter)
        {
            return Repository.SearchHotelsByApartments(searchTerm, searchParameter);
        }

    }
}
