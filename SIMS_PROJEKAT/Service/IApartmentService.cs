using Sims_Projekat.Model;
using Sims_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Service
{
    public interface IApartmentService
    {
        List<Apartment> GetAllApartments();
        void CreateApartment(Apartment apartment);
    }

    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository Repository;

        public ApartmentService(IApartmentRepository repository)
        {
            Repository = repository;
        }

        public List<Apartment> GetAllApartments()
        {
            return Repository.GetAllApartments();
        }

        public void CreateApartment(Apartment apartment)
        {
            Repository.CreateApartment(apartment);
        }
    }
}
