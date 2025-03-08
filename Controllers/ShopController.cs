using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Controllers
{
    public class ShopController : Controller
    {
        private readonly FoodContext _db;
        private readonly IShopService _shopService;
        private readonly IHotelService _hotelService;
        public ShopController(FoodContext db,IShopService shopService, IHotelService hotelService)
        {
            _db = db;
            _shopService = shopService;
            _hotelService = hotelService;
        }

        public async Task<IActionResult> ViewHotels([FromForm] int id)
        {
            var Hotel = await _shopService.ViewHotelDetails(id);
            if (Hotel == null)
            {
                return BadRequest();
            }
            return View("HotelDetail", Hotel);
        }

        public async Task<IActionResult> MovetoHome()
        {
            var Hotels = await _hotelService.GetAllHotels();
            return View("Marketplace", Hotels);
        }

    }
}
