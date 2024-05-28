using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS_PROJEKAT.Model
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ApartmentName { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus Status { get; set; }

        public string GuestJMBG { get; set; }

        public string HotelPassword { get; set; }

        public string Description { get; set; }
        
    }

    public enum ReservationStatus
    {
        Declined,
        Approved,
        OnHold
    }

   
}
