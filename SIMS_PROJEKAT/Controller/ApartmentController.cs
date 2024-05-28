using Microsoft.AspNetCore.Mvc;
using Sims_Projekat.Model;
using Sims_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController: ControllerBase
    {
        private readonly IApartmentService ApartmentService;

        public ApartmentController (IApartmentService apartmentService)
        {
            ApartmentService = apartmentService;
        }

        [HttpPost]
        public IActionResult CreateApartment(Apartment apartment)
        {
            ApartmentService.CreateApartment(apartment);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllApartments()
        {
            var Apartments = ApartmentService.GetAllApartments();
            return Ok(Apartments);
        }

    }
}
