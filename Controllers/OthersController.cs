using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;

namespace YetiMunch.Controllers
{
    public class OthersController : Controller
    {

        private readonly FoodContext _db;

        public object?[]? Id { get; private set; }

        public OthersController(FoodContext db)
        {
            _db = db;
        }
        public IActionResult MovetoLoginPage()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult GetEndUsers()
        {
            List<User> registeredUsers = _db.Users.ToList(); //Acessing the users from the db as it is saved as User type

            // Mapping User entities to UserDTO as the model.UsersDTO is accepted by the view
            List<UserDTO> userDTOs = registeredUsers.Select( user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username
            }).ToList();

            return View("Enduser", userDTOs);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID received");
            }

            var obj = await _db.Users.FindAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Users.Remove(obj);
            await _db.SaveChangesAsync();

            List<UserDTO> userDTOs = _db.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username
            }).ToList();


            return View("Enduser",userDTOs); 
        }


        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm]int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID received");
            }

            var _userData = await _db.Users.FindAsync(id);
            if (_userData == null)
            {
                return NotFound();
            }
            UserDTO userDTO = new UserDTO
            {
                Id = _userData.Id,
                Username = _userData.Username,
                Email = _userData.Email
            };

            return View("EditPage", userDTO);  
        }
        [HttpPost]
        public async Task<IActionResult>  EditPost(UserDTO _user)
        {

            if (_user == null || _user.Id==0)
            {
                return BadRequest("Invalid User!");
            }

            var _userDetails = await _db.Users.FindAsync(_user.Id);
            if (_userDetails == null)
            {
                return NotFound(_user);
            }
            _userDetails.Username = _user.Username;
            _userDetails.Email = _user.Email;

            await _db.SaveChangesAsync();
            List<UserDTO> userDTOs = _db.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username
            }).ToList();


            return View("Enduser", userDTOs);
        }

        public IActionResult BackToDashboard()
        {
            return View("Dashboard");
        }

    }
}
