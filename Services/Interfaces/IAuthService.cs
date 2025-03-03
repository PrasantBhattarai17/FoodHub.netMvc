using YetiMunch.Models;

namespace YetiMunch.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(UserDTO loginRequest);

        Task<(string?, List<HotelDto> hotels,UserDTO userDto)> Login(UserDTO requestedUser);

    }
}
