using Microsoft.EntityFrameworkCore;
using Sims_Projekat.Model;
using Sims_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Service
{
   public interface IUserService
    {
        List<User> GetAllUsers(); //admin
        User GetUserByJMBG(string jmbg); 
        bool CreateUser(User user); //admin
        User GetUserByEmail(string email);
        bool Login(string email, string password);
        List<User> SortUsersByFirstName(); //admin
        List<User> SortUsersByLastName(); //admin
        List<User> GetUsersByType(UserType userType); //admin
        bool BlockUser(string jmbg); //admin
        bool UnblockUser(string jmbg); //admin

    }

    public class UserService : IUserService
    {
        private readonly IUserRepository Repository;

        public UserService(IUserRepository repository)
        {
            Repository = repository;
        }

        public List<User> GetAllUsers()
        {
            return Repository.GetAllUsers();
        }

        public User GetUserByJMBG(string jmbg)
        {
            return Repository.GetUserByJMBG(jmbg);
        }
        
        public bool CreateUser(User user)
        {
            if(Repository.GetUserByEmail(user.Email) ==  null && Repository.GetUserByJMBG(user.JMBG) == null)
            {
                Repository.CreateUser(user);
                return true;
            }
            return false;
        }

        public User GetUserByEmail(string email)
        {
            return Repository.GetUserByEmail(email);
        }

        public bool Login(string email, string password)
        {
            var User = Repository.GetUserByEmail(email);
            return User != null && User.Password == password ? true : false;
        }

        public List<User> SortUsersByFirstName()
        {
            List<User> Users = Repository.GetAllUsers();
            return Users.OrderBy(u => u.FirstName).ToList();
        }

        public List<User> SortUsersByLastName()
        {
            List<User> Users = Repository.GetAllUsers();
            return Users.OrderBy(u => u.LastName).ToList();
        }

        public List<User> GetUsersByType(UserType userType)
        {
            return Repository.GetAllUsers().Where(u => u.UserType == userType).ToList();
        }

        public bool BlockUser(string jmbg)
        {
            return Repository.BlockUser(jmbg);
        }
        public bool UnblockUser(string jmbg)
        {
            return Repository.UnblockUser(jmbg);
        }


    }
}
