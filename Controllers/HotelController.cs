using Microsoft.AspNetCore.Mvc;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;
namespace YetiMunch.Controllers
{
    public class HotelController : Controller
    {
        private readonly FoodContext _db;

        private readonly IHotelService _hotelService;
        public HotelController(FoodContext db,IHotelService hotelService)
        {
            _db = db;
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _hotelService.GetAllHotels();
            return View("HotelPage",hotels);
        }
        public IActionResult GotoHotelPage()
        {
            return View("AddHotels");
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotels(Hotel hotel)
        {
            var result = await _hotelService.AddHotel(hotel);
            if (!result)
            {
                return BadRequest();
            }
            var hotelLists = await _hotelService.GetAllHotels();
            if (hotelLists == null)
            {
                return NotFound();
            }

            return View("HotelPage",hotelLists);
            
        }


        [HttpPost]
        public async Task<IActionResult> DeleteHotel([FromForm] int id)
        {
            var result = await _hotelService.RemoveHotel(id);
            if (!result)
            {
                return BadRequest();
            }
            var hotelLists = await _hotelService.GetAllHotels();
            if (hotelLists == null)
            {
                return NotFound();
            }
            return View("HotelPage", hotelLists);
        }

        public async Task<IActionResult> EditHotel([FromForm] int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel == null)
            {
                return BadRequest();
            }

            return View("EditHotel", hotel);
        }


        public async Task<IActionResult> EditHotelPost([FromForm] HotelDto hotel)
        {

            var result = await _hotelService.UpdateHotel(hotel);
            if (!result)
            {
                return BadRequest();
            }

            var hotelList = await _hotelService.GetAllHotels();

            return View("HotelPage", hotelList);
        }
    }
}
