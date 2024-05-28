using Sims_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Repository
{
    public interface IApartmentRepository
    {
        List<Apartment> GetAllApartments();
        void CreateApartment (Apartment apartment);
    }

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDBContext DbContext;

        public ApartmentRepository(ApplicationDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Apartment> GetAllApartments()
        {
            return DbContext.Apartment.ToList();
        }

        public void CreateApartment(Apartment apartment)
        {
            DbContext.Apartment.Add(apartment);
            DbContext.SaveChanges();
        }
    }
}
