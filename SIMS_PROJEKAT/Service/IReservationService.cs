using SIMS_PROJEKAT.Model;
using SIMS_PROJEKAT.Repository;

namespace SIMS_PROJEKAT.Service
{
    public interface IReservationService
    {
        List<Reservation> GetAllReservations();
        bool CancelReservation(int id);
        void CreateReservation(Reservation reservation);
        public List<Reservation> GetReservationsByGuestJMBG(string jmbg);
        public List<Reservation> SortReservationsByStatus(string jmbg, string status); // za guesta
        public List<Reservation> GetNotDeclinedReservations();
        public List<Reservation> SortReservationsByStatusForOwner(string status);
        public bool CanCreateReservation(Reservation reservation);
        public List<Reservation> GetNotDeclinedReservationsByGuestJMBG(string JMBG);
        public List<Reservation> GetReservationsByHotelPassword(string password);
        void UpdateReservation(int reservationId, string status, string description);
    }

    public class ReservationService: IReservationService
    {
        private readonly IReservationRepository Repository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            Repository = reservationRepository;
        }

        public List<Reservation> GetAllReservations()
        {
            return Repository.GetAllReservations();
        }

        public bool CancelReservation(int id)
        {
            return Repository.CancelReservation(id);
        }

        public void CreateReservation(Reservation reservation)
        {
            Repository.CreateReservation(reservation);
           
        }

        public List<Reservation> GetReservationsByGuestJMBG(string JMBG)
        {
            return Repository.GetReservationsByGuestJMBG(JMBG);
        }

        public List<Reservation> GetReservationsByHotelPassword(string password)
        {
            var Hotels = Repository.GetAllReservations();
            return Hotels.Where(h => h.HotelPassword == password && h.Status == ReservationStatus.OnHold).ToList();
        }

        public List<Reservation> SortReservationsByStatus(string jmbg, string status)
        {
            return Repository.SortReservationsByStatus(jmbg, status);
        }

        public List<Reservation> GetNotDeclinedReservations()
        {
            return Repository.GetNotDeclinedReservations();
        }

        public List<Reservation> GetNotDeclinedReservationsByGuestJMBG(string jmbg)
        {
            var Reservations = Repository.GetReservationsByGuestJMBG(jmbg).ToList();
            return Reservations.Where(r => r.Status != ReservationStatus.Declined).ToList();
        }

        public  List<Reservation> SortReservationsByStatusForOwner(string status)
        {
            return Repository.SortReservationsByStatusForOwner(status);
        }

        public bool CanCreateReservation(Reservation reservation)
        {
            List<Reservation> Reservations = Repository.GetAllReservations();
            var FoundReservation = Reservations.Find(r => r.ReservationDate.DayOfYear == reservation.ReservationDate.DayOfYear && r.ApartmentName == reservation.ApartmentName && r.Status == ReservationStatus.Approved);
            return FoundReservation != null ? false : true;

        }

        public void UpdateReservation(int reservationId, string status, string description)
        {
            Reservation Reservation = Repository.GetReservationByID(reservationId);

           if(Reservation == null)
           {
                throw new Exception("Resevation not found");
           }

           if (Enum.TryParse(status, out ReservationStatus statusEnum))
           {
                Reservation.Status = statusEnum;
                Reservation.Description = description;
                Reservation.ReservationDate = DateTime.SpecifyKind(Reservation.ReservationDate, DateTimeKind.Utc);
           }

           Repository.UpdateReservation(Reservation);
        }
     }
}
