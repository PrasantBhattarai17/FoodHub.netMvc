using YetiMunch.Entities;
using YetiMunch.Models;

namespace YetiMunch.Repository.IRepository
{
    public interface IAuthRepository:IRepository<User>
    {
        Task<bool> IsUserExists(string username, string email);

        Task<User?> GetUserByUsername(string username);

        Task<List<HotelDto>> GetHotels();

    }
}
