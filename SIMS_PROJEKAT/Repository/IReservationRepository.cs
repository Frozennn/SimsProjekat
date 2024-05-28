using Sims_Projekat;
using Sims_Projekat.Model;
using SIMS_PROJEKAT.Model;

namespace SIMS_PROJEKAT.Repository
{
    public interface IReservationRepository
    {
        List<Reservation> GetAllReservations();
        bool CancelReservation(int id);
        void CreateReservation(Reservation reservation);
        List<Reservation> GetReservationsByGuestJMBG(string jmbg);
        List<Reservation> SortReservationsByStatus(string jmbg,string status);
        List<Reservation> GetNotDeclinedReservations();
        List<Reservation> SortReservationsByStatusForOwner(string status);
        void UpdateReservation(Reservation reservation);
        Reservation GetReservationByID(int id);

    }

    public class ReservationRepository: IReservationRepository
    {
        private readonly ApplicationDBContext DbContext;

        public ReservationRepository(ApplicationDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public Reservation GetReservationByID(int id)
        {
              return DbContext.Reservation.FirstOrDefault(reservation => reservation.Id == id);
        }

        public List<Reservation> GetAllReservations()
        {
            return DbContext.Reservation.ToList();
        }

        public bool CancelReservation(int id)
        {
            var Reservation = DbContext.Reservation.FirstOrDefault(reservation => reservation.Id == id);

            if(Reservation == null)
            {
                return false;
            }              

            if(Reservation.Status != ReservationStatus.Declined)
               {
                    Reservation.Status = ReservationStatus.Declined;
                    DbContext.SaveChanges();
                    return true;
               }
            return false;
        }
        public void CreateReservation(Reservation reservation)
        {
            DbContext.Reservation.Add(reservation);
            DbContext.SaveChanges();
        }

        public List<Reservation> GetReservationsByGuestJMBG(string jmbg)
        {
            return DbContext.Reservation.Where(r => r.GuestJMBG == jmbg).ToList();
        }

       public List<Reservation> SortReservationsByStatus(string jmbg,string status)
        {
            var Reservations = GetReservationsByGuestJMBG(jmbg);

            switch (status)
            {
                case "Approved":
                    return Reservations.Where(r => r.Status == ReservationStatus.Approved).ToList();
                case "OnHold":
                        return Reservations.Where(r => r.Status == ReservationStatus.OnHold).ToList();
                case "Declined":
                    return Reservations.Where(r => r.Status == ReservationStatus.Declined).ToList();
                default:
                    throw new ArgumentException("Nepoznat Parametar pretrage");

            }
        }

        public List<Reservation> GetNotDeclinedReservations()
        {
            var Reservations = GetAllReservations();
            return Reservations.Where(r => r.Status != ReservationStatus.Declined).ToList();
        }

        public List<Reservation> SortReservationsByStatusForOwner(string status)
        {
            var Reservations = GetAllReservations();
            switch (status)
            {
                case "Approved":
                    return Reservations.Where(r => r.Status == ReservationStatus.Approved).ToList();
                case "OnHold":
                    return Reservations.Where(r => r.Status == ReservationStatus.OnHold).ToList();
                default:
                    throw new ArgumentException("Nepoznat Parametar pretrage");

            }
        }
        public void UpdateReservation(Reservation reservation)
        {
            DbContext.Reservation.Update(reservation);
            DbContext.SaveChanges();
        }
    }

    
}
