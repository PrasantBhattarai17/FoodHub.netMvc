using YetiMunch.Entities;
using YetiMunch.Models;

namespace YetiMunch.Services.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelDto>> GetAllHotels();
        Task<HotelDto> GetHotelById(int id);
        Task<bool> RemoveHotel(int id);
        Task<bool> UpdateHotel(HotelDto hotel);
        Task<bool> AddHotel(Hotel hotel);


    }
       
}
