using Microsoft.AspNetCore.Mvc;
using SIMS_PROJEKAT.Model;
using SIMS_PROJEKAT.Service;

namespace SIMS_PROJEKAT.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController: ControllerBase
    {

        private readonly IReservationService ReservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.ReservationService = reservationService;
        }

        [HttpPost]
        public IActionResult CreateReservation(Reservation reservation)
        {
            if(ReservationService.CanCreateReservation(reservation))
            {
                ReservationService.CreateReservation(reservation);
                return Ok();
            }
            else
            {
                return BadRequest("The apartment is already booked for the selected date");
            }
        }

        [HttpGet]
        public List<Reservation> GetAllReservations()
        {
            return ReservationService.GetAllReservations();
        }

        [HttpGet("GuestJMBG")]
        public List<Reservation> GetReservationsByGuestJMBG(string jmbg)
        {
            return ReservationService.GetReservationsByGuestJMBG(jmbg);
        }
        [HttpGet("HotelPassword")]
        public List<Reservation> GetReservationsByHotelPassword(string password)
        {
            return ReservationService.GetReservationsByHotelPassword(password);
        }


        [HttpGet("SortByStatus")]
        public List<Reservation> SortReservationsByStatus(string jmbg, string status)
        {
            return ReservationService.SortReservationsByStatus(jmbg, status);
        }
        [HttpGet("NotCancelled")]
        public List<Reservation> GetNotDeclinedReservations()
        {
            return ReservationService.GetNotDeclinedReservations();
        }

        [HttpGet("NotCancelledByGuestJMBG")]
        public List<Reservation> GetNotDeclinedReservationsByGuestJMBG(string jmbg)
        {
            return ReservationService.GetNotDeclinedReservationsByGuestJMBG(jmbg);
        }
        [HttpGet("SortByStatusForOwner")]
        public List<Reservation> SortReservationsByStatusForOwner(string status)
        {
            return ReservationService.SortReservationsByStatusForOwner(status);
        }
        [HttpGet("Cancel")]
        public IActionResult CancelReservation(int id)
        {
            bool IsCancelled = ReservationService.CancelReservation(id);
            return IsCancelled ? Ok() : BadRequest("Nije uspelo otkazivanje rezervacije");     
        }

        [HttpPut("{reservationId}")]
        public IActionResult UpdateReservation(int reservationId, [FromBody] UpdateReservationRequest request)
        {
            try
            {
                ReservationService.UpdateReservation(reservationId, request.Status, request.Description);
                return Ok("Reservation updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




    }




}
