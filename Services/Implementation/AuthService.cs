using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly FoodContext _db;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(FoodContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }
        public async Task<bool> Register(UserDTO loginRequest)
        {
            if (await _db.Users.AnyAsync(u => u.Username == loginRequest.Username) ||
           await _db.Users.AnyAsync(u => u.Email == loginRequest.Email))
            {
                return false;
            }

            var hashedPassword = _passwordHasher.HashPassword(new User(), loginRequest.Password);

            var user = new User
            {
                Username = loginRequest.Username,
                Email = loginRequest.Email,
                PasswordH = hashedPassword
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task<(string?,List<HotelDto> hotels,UserDTO userDto)> Login(UserDTO requestedUser)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == requestedUser.Username);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordH, requestedUser.Password) == PasswordVerificationResult.Failed)
            {
                return (null,new List<HotelDto>(),null);
            }
            string? token = GenerateToken(user);
            UserDTO userDto = new UserDTO
            {
                Username = user.Username,
                Email = user.Email
            };

            var hotels = await _db.Hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                HotelImg = h.HotelImg,
                category = h.category,
                Cuisines = h.Cuisines,
                Discount = h.Discount,
                Price = h.Price,
                Location = h.Location,
                Rating = h.Rating
            }).ToListAsync();

            return (token, hotels,userDto);
        }

        private string GenerateToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["jwtkey"]));

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["issuer"],
                audience: jwtSettings["Auidence"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
