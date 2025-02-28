using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Models;

namespace YetiMunch.Controllers
{
    public class ShopController : Controller
    {
        private readonly FoodContext _db;
        public ShopController(FoodContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ViewHotels([FromForm] int id)
        {
            var _hotel =await _db.Hotels
                .Include(h => h.Foods) 
                .FirstOrDefaultAsync(h => h.HotelId == id);

            if (_hotel == null)
            {
                return NotFound();
            }

            HotelDto _hoteldetails = new HotelDto
            {
                HotelId = _hotel.HotelId,
                HotelName = _hotel.HotelName,
                HotelImg = _hotel.HotelImg,
                category = _hotel.category,
                Cuisines = _hotel.Cuisines,
                Discount = _hotel.Discount,
                Price = _hotel.Price,
                Location = _hotel.Location,
                Rating = _hotel.Rating,
                Foods = _hotel.Foods.Select(f => new FoodDto
                {
                    FoodId = f.FoodId,
                    FoodName = f.FoodName,
                    Description = f.Description,
                    Cost = f.Cost,
                    Amount = f.Amount,
                    Category = f.Category,
                    Discount = f.Discount,
                    FoodImgUrl=f.FoodImgUrl,
                }).ToList()
            };

            return View("HotelDetail", _hoteldetails);
        }

        public async Task<IActionResult> MovetoHome()
        {
            List<HotelDto> _hoteldetails =await _db.Hotels.Select(_hotel => new HotelDto
            {
                HotelId = _hotel.HotelId,
                HotelName = _hotel.HotelName,
                HotelImg = _hotel.HotelImg,
                category = _hotel.category,
                Cuisines = _hotel.Cuisines,
                Discount = _hotel.Discount,
                Price = _hotel.Price,
                Location = _hotel.Location,
                Rating = _hotel.Rating
            }).ToListAsync();
            return View("Marketplace", _hoteldetails);
        }

    }
}
