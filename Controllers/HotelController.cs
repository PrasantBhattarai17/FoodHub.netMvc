using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
//Controller for Hotels and the food 
namespace YetiMunch.Controllers
{
    public class HotelController : Controller
    {
        private readonly FoodContext _db;
        public HotelController(FoodContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            List<Hotel> _hotel =await _db.Hotels.ToListAsync();
            List<HotelDto> _hoteldetails= _hotel.Select(hotelList => new HotelDto
            {
                HotelId = hotelList.HotelId,
                Location=hotelList.Location,
                HotelName = hotelList.HotelName,
                HotelImg = hotelList.HotelImg,
                category = hotelList.category,
                Cuisines = hotelList.Cuisines,
                Discount = hotelList.Discount,
                Rating=hotelList.Rating,
                Price=hotelList.Price
            }).ToList();
            return View("HotelPage",_hoteldetails);
        }
        public IActionResult GotoHotelPage()
        {
            return View("AddHotels");
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotels(Hotel _hotel)
        {
            if (_hotel == null)
            {
                return View("HotelsPage", _hotel);
            }

            await _db.Hotels.AddAsync(_hotel);
            _db.SaveChanges();

            List<Hotel> _allhotels =await _db.Hotels.ToListAsync();
            List<HotelDto> _hoteldetails = _allhotels.Select(hotelList => new HotelDto
            {
                HotelId = hotelList.HotelId,
                HotelName = hotelList.HotelName,
                HotelImg = hotelList.HotelImg,
                category = hotelList.category,
                Cuisines = hotelList.Cuisines,
                Discount = hotelList.Discount,
                Price = hotelList.Price,
                Location = hotelList.Location,
                Rating=hotelList.Rating


            }).ToList();

            return View("HotelPage",_hoteldetails);
            
        }


        [HttpPost]
        public async Task<IActionResult> DeleteHotel([FromForm] int id)
        {
            var obj =await  _db.Hotels.FindAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Hotels.Remove(obj);
            await _db.SaveChangesAsync();

            List<HotelDto> _hoteldetails =await _db.Hotels.Select(_hotel => new HotelDto {
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


            return View("HotelPage", _hoteldetails);
        }

        public async Task<IActionResult> EditHotel([FromForm] int id)
        {
            var _hotel = await _db.Hotels.FindAsync(id);
            if (_hotel == null)
            {
                return NotFound();
            }
            HotelDto _HotelDetail = new HotelDto
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
            };

            return View("EditHotel", _HotelDetail);
        }


        public async Task<IActionResult> EditHotelPost([FromForm] HotelDto _hotel)
        {
            if (_hotel == null)
            {
                return NotFound();
            }
            var _editedHotel = await _db.Hotels.FindAsync(_hotel.HotelId);
            if (_editedHotel == null)
            {
                return NotFound(_editedHotel);
            }
            _editedHotel.HotelName = _hotel.HotelName;
                _editedHotel.HotelImg = _hotel.HotelImg;
                _editedHotel.category = _hotel.category;
               _editedHotel.Cuisines = _hotel.Cuisines;
                _editedHotel.Discount = _hotel.Discount;
                _editedHotel.Price = _hotel.Price;
                _editedHotel.Location = _hotel.Location;
                _editedHotel.Rating = _hotel.Rating;

     
            await _db.SaveChangesAsync();


            List<HotelDto> HotelList =await  _db.Hotels.Select(_hotel =>
            new HotelDto
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


            return View("HotelPage", HotelList);
        }
    }
}
