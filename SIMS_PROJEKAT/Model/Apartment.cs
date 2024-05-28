using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Model
{
   public class Apartment
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public int MaxGuestNumber { get; set; }

        public string OwnerJMBG { get; set; }
        public string HotelPassword { get; set; }
    }
}
