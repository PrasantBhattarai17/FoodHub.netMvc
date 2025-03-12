using YetiMunch.Models;

namespace YetiMunch.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<bool> DeleteUser(int id);

        Task<UserDTO?> GetUserById(int id);

        Task<bool> UpdateUser(UserDTO user);


    }
}
