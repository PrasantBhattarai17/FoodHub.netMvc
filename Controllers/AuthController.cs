using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;

namespace YetiMunch.Controllers
{
    public class AuthController : Controller
    {

        private readonly FoodContext _db;

        private readonly IConfiguration _config;
        public AuthController(FoodContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }



        [HttpPost]
        public async Task<IActionResult> Register(UserDTO LoginRequest) //register method
        {
           

            if (_db.Users.Any(u => u.Username == LoginRequest.Username))
            {
                ModelState.AddModelError("Username", "Username already taken");
                return View(LoginRequest);
            }

            if (_db.Users.Any(u => u.Email == LoginRequest.Email))
            {
                ModelState.AddModelError("Email", "Email address already taken");
                return View(LoginRequest);
            }

            var HashedPassword = new PasswordHasher<User>().HashPassword(new User(), LoginRequest.Password);

            var user = new User
            {
                Username = LoginRequest.Username,
                Email = LoginRequest.Email,
                PasswordH = HashedPassword
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return View("Login");

        }

        [HttpPost]
        public IActionResult Login(UserDTO RequestedUser)
        {
         

            var user = _db.Users.FirstOrDefault(u => u.Username == RequestedUser.Username);

            if (user == null)
            {
                ModelState.AddModelError("Username", "Invallid Username!");
                return View(RequestedUser);
            }
            var isMatched = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordH, RequestedUser.Password);


            if (isMatched == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View(RequestedUser);
            }

            var token = GenerateToken(user);

            if (user.Username == "admin" && user.Email == "admin@admin")
            {

                return View("Dashboard");

            }
            else
            {
                List<HotelDto> _hoteldetails = _db.Hotels.Select(_hotel => new HotelDto
                {
                    HotelId = _hotel.HotelId,
                    HotelName = _hotel.HotelName,
                    HotelImg = _hotel.HotelImg,
                    category = _hotel.category,
                    Cuisines = _hotel.Cuisines,
                    Discount = _hotel.Discount,
                    Price = _hotel.Price,
                    Location = _hotel.Location,
                    Rating = _hotel.Rating
                }).ToList();
                return View("Marketplace",_hoteldetails);
            }
        }
        //Method to generate the jwt token for the user after sucessful login
        private string GenerateToken(User _user)
        {
            var jwtSettings = _config.GetSection("Jwt"); // accesing the jwt variable we created in the application.json file
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["jwtkey"])); // assigning the unique secret key and converting it from stirng to bytes for encryption


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Username), //providing the username of the user that needs to genereate the token
                new Claim(ClaimTypes.Role, "User"), //assigning the role to the user (rolebased authorization)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // adding the uniquee identity in the token such that every token are unique
            };


            var token = new JwtSecurityToken // creating the token here
            (
                issuer: jwtSettings["issuer"], //the one who issues the token
                audience: jwtSettings["Auidence"],//the one that uses the token 
                claims: claims, //providing all the roles we configured earlier 
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])), //lifspan of the token 
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256) //uses Hmachsha256 encryption
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // finally returning the jwt object token after converting it into string

        }

        
    }

}
