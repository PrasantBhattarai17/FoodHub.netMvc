using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService( IConfiguration config,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }
        public async Task<bool> Register(UserDTO loginRequest)
        {
            if (await _unitOfWork.auth.IsUserExists(loginRequest.Username,loginRequest.Email))
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
            using var transaction = await _unitOfWork.BeginTransaction();
            {
                try
                {

                    await _unitOfWork.auth.Add(user);
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch(Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<(string?,List<HotelDto> hotels,UserDTO userDto)> Login(UserDTO requestedUser)
        {
            var user = await _unitOfWork.auth.GetUserByUsername(requestedUser.Username);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordH, requestedUser.Password) == PasswordVerificationResult.Failed)
            {
                return (null,new List<HotelDto>(), new UserDTO());
            }
            string? token = GenerateToken(user);
            UserDTO userDto = new UserDTO
            {
                Username = user.Username,
                Email = user.Email
            };

            var hotels = await _unitOfWork.auth.GetHotels();

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
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
