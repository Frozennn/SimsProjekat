using Microsoft.AspNetCore.Mvc;
using Sims_Projekat.Model;
using Sims_Projekat.Service;
using SIMS_PROJEKAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService HotelService;

        public HotelController(IHotelService hotelService)
        {
            HotelService = hotelService;
        }

        [HttpPost]
        public IActionResult CreateHotel(Hotel hotel)
        {
            HotelService.CreateHotel(hotel);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var Hotels = HotelService.GetAllHotels();
            return Ok(Hotels);
        }
        [HttpGet("GetAllOwnersHotels")]
        public List<Hotel> GetAllOwnersHotels(string jmbg)
        {
            return HotelService.GetAllOwnersHotels(jmbg);
        }

        [HttpGet]
        [Route("api/[controller]/approvedHotels")]
        public List<Hotel> GetAllApprovedHotels() { 
            return HotelService.GetAllApprovedHotels();
        }

        [HttpGet]
        [Route("sortByName")]
        public List<Hotel> SortHotelsByName()
        {
            return HotelService.SortHotelsByName();
        }

        [HttpGet]
        [Route("sortByStars")]
        public List<Hotel> SortHotelsByStars()
        {
            return HotelService.SortHotelsByStars();
        }

        [HttpPut("{hotelPassword}")]
        public IActionResult UpdateHotel(string hotelPassword, [FromBody] UpdateHotelRequest request)
        {
            try
            {
                HotelService.UpdateHotel(hotelPassword, request.Status);
                return Ok("Hotel updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public List<Hotel> SearchHotels(string searchTerm, string searchParameter)
        {
            var Hotels = HotelService.SearchHotels(searchTerm, searchParameter);
            return Hotels;
        }

        [HttpGet("searchApartments")]
        public List<Hotel> SearchHotelsByApartments(string searchTerm, string searchParameter)
        {
            var Hotels = HotelService.SearchHotelsByApartments(searchTerm, searchParameter);
            return Hotels;
        }

    }
}
