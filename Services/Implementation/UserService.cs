using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Repository.IRepository;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var user = await _userRepository.GetAll();
            var userLists = user.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username
            }).ToList();
            return userLists;

        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
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
            var user = await _userRepository.GetById(id);
            if (user == null || (user.Username=="admin" && user.Email == "admin@admin"))
            {
                return false;
            }

            await _userRepository.Delete(id); 
            return true;
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {
            var existingUser = await _userRepository.GetById(user.Id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;

            await _userRepository.Update(existingUser);
            return true;
        }
    }
}
