using Sims_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserByJMBG(string jmbg);
        void CreateUser(User user);
        User GetUserByEmail(string Email);
        bool BlockUser(string jmbg);
        bool UnblockUser(string jmbg);

    }

    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDBContext DbContext;

        public UserRepository(ApplicationDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<User> GetAllUsers()
        {
            return DbContext.Users.ToList();
        }

        public User GetUserByJMBG(string jmbg)
        {
            return DbContext.Users.FirstOrDefault(user => user.JMBG == jmbg);
        }

        public void CreateUser(User user) { 
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return DbContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public bool BlockUser(string jmbg)
        {
           User User = GetUserByJMBG(jmbg);
            if(User != null)
            {
                User.IsBlocked = true;
                DbContext.SaveChanges();
                return true;
            }
            return false;
          
        }
        public bool UnblockUser(string jmbg)
        {
            User User = GetUserByJMBG(jmbg);
            if (User != null)
            {
                User.IsBlocked =false;
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
