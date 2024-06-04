using Microsoft.AspNetCore.Mvc;
using PSW_main_1.Model;
using Sims_Projekat.Model;
using Sims_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Projekat.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            bool IsUserCreated = UserService.CreateUser(user);
            return IsUserCreated? Ok() : BadRequest("User Registration failed, User with the same email or JMBG already exists.");
            
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var Users = UserService.GetAllUsers();
            return Ok(Users);
        }

        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var User = UserService.GetUserByEmail(email);
            return  User == null ? NotFound("User not found") : Ok(User); 
        }

        [HttpGet("{jmbg}")]
        public IActionResult GetUserByJMBG(string jmbg)
        {
            var User = UserService.GetUserByJMBG(jmbg);
            return Ok(User);

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            bool LoginResult = UserService.Login(loginRequest.Email, loginRequest.Password);

            if(LoginResult)
            {
                var User = UserService.GetUserByEmail(loginRequest.Email);
                if(User != null && User.IsBlocked)
                {
                    return Unauthorized("Your account is blocked.");
                }
                return Ok();
            }
            else
            {
                return Unauthorized("Invalid Credentials");
            }
        }

        [HttpGet]
        [Route("sortByFirstName")]
        public List<User> SortUsersByFirstName()
        {
            return UserService.SortUsersByFirstName();
        }

        [HttpGet]
        [Route("sortByLastName")]
        public List<User> SortUsersByLastName()
        {
            return UserService.SortUsersByLastName();
        }

        [HttpGet("ByType")]
        public IActionResult GetUsersByType(UserType userType)
        {
            var Users = UserService.GetUsersByType(userType);
            return Ok(Users);
        }

        [HttpPost("Block")]
        public IActionResult BlockUser(string jmbg)
        {
            bool IsBlocked = UserService.BlockUser(jmbg);
            return IsBlocked ? Ok("User blocked successfully") : NotFound("User not found.");
        }

        [HttpPost("Unblock")]
        public IActionResult UnblockUser(string jmbg)
        {
            bool IsUnblocked = UserService.UnblockUser(jmbg);
            return IsUnblocked ? Ok("User unblocked successfully.") : NotFound("User not found.");
        }
    }
}
