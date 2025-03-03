using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly FoodContext _db;

        public UserService(  FoodContext db)
        {
            _db = db;
        }
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var user = await _db.Users.ToListAsync();
            var userLists = user.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username
            }).ToList();
            return userLists;

        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return null;

            UserDTO? thatUser = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
            return thatUser;

        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null || (user.Username=="admin" && user.Email == "admin@admin"))
            {
                return false;
            }

             _db.Remove(user);
            await _db.SaveChangesAsync();
             
            return true;
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {
            var existingUser =await _db.Users.FindAsync(user.Id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
