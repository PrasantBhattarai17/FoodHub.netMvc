using Microsoft.AspNetCore.Mvc;
using YetiMunch.Data;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Controllers
{
    public class OthersController : Controller
    {

        private readonly IUserService _userService;
        private readonly FoodContext _db;

        public OthersController(IUserService userService, FoodContext db)
        {
            _db = db;
            _userService = userService;
        }
        public IActionResult MovetoLoginPage()
        {
            return View("Login");
        }

        [HttpGet]
        public async Task<IActionResult> GetEndUsers()
        {
            var users=await _userService.GetAllUsers();
            return View("EndUser", users);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] int id)
        {
            var result = await _userService.DeleteUser(id);
            var users = await _userService.GetAllUsers();
            if (!result)
            {
                ViewBag.ErrorMessage("Cannot delete this user!");
                return View("EndUser", users);
            }

            return View("EndUser", users);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm]int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View("EditPage", user);
        }
        [HttpPost]
        public async Task<IActionResult>  EditPost(UserDTO userDto)
        {
            var result = await _userService.UpdateUser(userDto);

            if (!result)
            {

                return BadRequest();
            }
            var userList = await _userService.GetAllUsers();

            return View("Enduser", userList);
             }

        public IActionResult BackToDashboard()
        {
            return View("Dashboard");
        }
         
        public IActionResult Signout()
        {
            return View("Login");
        }
        public IActionResult BackToRegisterPage()
        {
            return View("Register");
        }
    }
}
