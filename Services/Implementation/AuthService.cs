using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Repository.IRepository;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }
        public async Task<bool> Register(UserDTO loginRequest)
        {
            if (await _authRepository.IsUserExists(loginRequest.Username,loginRequest.Email))
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

            await _authRepository.Add(user);

            return true;
        }
        public async Task<(string?,List<HotelDto> hotels,UserDTO userDto)> Login(UserDTO requestedUser)
        {
            var user = await _authRepository.GetUserByUsername(requestedUser.Username);
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

            var hotels = await _authRepository.GetHotels();

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
