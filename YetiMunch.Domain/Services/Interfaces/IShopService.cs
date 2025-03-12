using YetiMunch.Models;

namespace YetiMunch.Services.Interfaces
{
    public interface IShopService
    {
         Task<HotelDto> ViewHotelDetails(int id);
    }
}
