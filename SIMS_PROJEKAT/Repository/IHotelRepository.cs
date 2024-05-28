using Sims_Projekat.Model;
using SIMS_PROJEKAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Repository
{
    public interface IHotelRepository
    {
        List<Hotel> GetAllHotels();
        void CreateHotel(Hotel hotel);
        List<Hotel> GetAllApprovedHotels();
        List<Hotel> SearchHotels(string searchTerm, string searchParameter);
        List<Hotel> SearchHotelsByApartments(string searchTerm, string searchParameter);
        void UpdateHotel(Hotel hotel);
        Hotel GetHotelByPassword(string password);
    }

    public class HotelRepository: IHotelRepository
    {
        private readonly ApplicationDBContext DbContext;

        public HotelRepository(ApplicationDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Hotel> GetAllHotels()
        {
            return DbContext.Hotel.ToList();
        }

        public List<Apartment> GetAllApartments()
        {
            return DbContext.Apartment.ToList();
        }

        public  List<Hotel> GetAllApprovedHotels()
        {
            List<Hotel> Hotels = GetAllHotels();
            List<Hotel> ApprovedHotels = new List<Hotel>();
            
            foreach(var Hotel in Hotels)
            { 
                if(Hotel.HotelStatus == HotelStatus.Approved)
                {
                    ApprovedHotels.Add(Hotel);
                }
            }
            return ApprovedHotels;
        }

        public void CreateHotel(Hotel hotel)
        {
            DbContext.Hotel.Add(hotel);
            DbContext.SaveChanges();
        }

        public List<Hotel> SearchHotels(string searchTerm, string searchParameter)
        {
            List<Hotel> Hotels = GetAllHotels();

            switch (searchParameter.ToLower())
            {
                case "password":
                    return Hotels.Where(h => h.Password.ToLower().Contains(searchTerm.ToLower())).ToList();
                case "name":
                    return Hotels.Where(h => h.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                case "yearofconstruction":
                    return Hotels.Where(h => h.YearOfConstruction.ToString().Contains(searchTerm)).ToList();
                case "numberofstars":
                    return Hotels.Where(h => h.NumberOfStars.ToString().Contains(searchTerm)).ToList();
                default:
                    throw new ArgumentException("Nepoznat parametar pretrage.");
            }
        }

        //Pomocna funkcija
        public List<Hotel> FilterHotels(List<Hotel> Hotels, List<Apartment> FilteredApartments)
        {
            List<Hotel> FilteredHotels = new List<Hotel>();
            foreach (var Apartment in FilteredApartments)
            {
                var Hotel = Hotels.FirstOrDefault(h => h.Password == Apartment.HotelPassword);
                if (Hotel != null && !FilteredHotels.Contains(Hotel))
                {
                    FilteredHotels.Add(Hotel);
                }
            }
            return FilteredHotels;
        }

        public List<Hotel> SearchHotelsByApartments(string searchTerm, string searchParameter)
        {
            List<Hotel> Hotels = GetAllHotels();
            List<Apartment> Apartments = GetAllApartments();

            Apartments.Where(a => a.NumberOfRooms.Equals(searchTerm));

            switch (searchParameter)
            {
                case "NumberOfRooms":
                    List<Apartment> FilteredApartments = Apartments.Where(a => a.NumberOfRooms == int.Parse(searchTerm)).ToList();
                    List<Hotel> FilteredHotels = FilterHotels(Hotels, FilteredApartments);
                    return FilteredHotels;
                case "MaxGuestNumber":
                    List<Apartment> FilteredApartments2 = Apartments.Where(a => a.MaxGuestNumber == int.Parse(searchTerm)).ToList();
                    List<Hotel> FilteredHotels2 = FilterHotels(Hotels, FilteredApartments2);
                    return FilteredHotels2;
                case "RoomAndGuestCount":
                    string FirstChar = searchTerm.Substring(0, 1);
                    string SecondChar = searchTerm.Substring(1, 1);
                    string LastChar = searchTerm.Substring(searchTerm.Length - 1);
                    if (SecondChar == "&")
                    {
                        List<Apartment> FilteredApartments3 = Apartments.Where(a => a.NumberOfRooms == int.Parse(FirstChar) && a.MaxGuestNumber == int.Parse(LastChar)).ToList();
                        List<Hotel> FilteredHotels3 = FilterHotels(Hotels, FilteredApartments3);
                        return FilteredHotels3;
                    }
                    else if (SecondChar == "|")
                    {
                        List<Apartment> FilteredApartments4 = Apartments.Where(a => a.NumberOfRooms == int.Parse(FirstChar) || a.MaxGuestNumber == int.Parse(LastChar)).ToList();
                        List<Hotel> FilteredHotels4 = FilterHotels(Hotels, FilteredApartments4);
                        return FilteredHotels4;
                    }
                    else throw new ArgumentException("Nepoznat operator pretrage");

                default:
                    throw new ArgumentException("Nepoznat parametar pretrage.");
            }
        }


        public void UpdateHotel(Hotel hotel)
        {
            DbContext.Hotel.Update(hotel);
            DbContext.SaveChanges();
        }

        public Hotel GetHotelByPassword(string password)
        {
            return DbContext.Hotel.FirstOrDefault(h => h.Password == password);
        }


    }
}
