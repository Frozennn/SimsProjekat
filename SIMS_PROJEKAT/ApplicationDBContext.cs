using Microsoft.EntityFrameworkCore;
using Sims_Projekat.Model;
using SIMS_PROJEKAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat
{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Apartment> Apartment { get; set; }

        public DbSet<Reservation> Reservation { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
 

    }

   
}
