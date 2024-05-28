using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Model
{
    public  class User
    {
        [Key]
        public string JMBG { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }

        public bool IsBlocked { get; set; }
    }

    public enum UserType
    {
        Administrator,
        Guest,
        Owner
    }
}
