using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Model
{
    public  class Hotel
    {
        [Key]
        public string Password { get; set; }
        public string Name { get; set; }

        public int  YearOfConstruction { get; set; }
        public Dictionary<string,Apartment> Apartments;
        public int NumberOfStars { get; set; }
        public string Owner { get; set; }  // JMBG vlasnika

        public HotelStatus HotelStatus { get; set; }

    }

    public enum HotelStatus
    {
        Declined,
        Approved,
        OnHold
    }
}
