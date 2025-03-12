using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var user = await _unitOfWork.Repository<User>().GetAll();
            var userLists = user.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username
            }).ToList();
            return userLists;

        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            var user = await _unitOfWork.Repository<User>().GetById(id);
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
            var user = await _unitOfWork.Repository<User>().GetById(id);
            if (user == null || (user.Username=="admin" && user.Email == "admin@admin"))
            {
                return false;
            }

            await _unitOfWork.Repository<User>().Delete(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {

            using var transaction =await _unitOfWork.BeginTransaction();
            {
                try
                {

                var existingUser = await _unitOfWork.Repository<User>().GetById(user.Id);

                if (existingUser == null)
                {
                    return false;
                }

                existingUser.Username = user.Username;
                existingUser.Email = user.Email;

                await _unitOfWork.Repository<User>().Update(existingUser);
                await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
