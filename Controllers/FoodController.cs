using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;

namespace YetiMunch.Controllers
{
    public class FoodController : Controller
    {
        private readonly FoodContext _db;
        public FoodController(FoodContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ListOfFoods()
        {
            List<Food> _foodList =await _db.Foods.Include(f=>f.Hotel).ToListAsync();

            List<FoodDto> _ListofFoods = _foodList.Select(f => new FoodDto
            {
                FoodId = f.FoodId,
                FoodName = f.FoodName,
                Description = f.Description,
                Cost = f.Cost,
                Amount = f.Amount,
                Category = f.Category,
                Discount = f.Discount,
                HotelId = f.HotelId,
                HotelName = f.Hotel.HotelName
            }).ToList();


            return View(_ListofFoods);
        }


        public async Task<IActionResult> AddaFood()
        {
            var hotels =await _db.Hotels
                .Select(h => new HotelDto
                {
                    HotelId = h.HotelId,
                    HotelName = h.HotelName
                })
                .ToListAsync();

            // Log the hotels to see if they are being fetched correctly
            Console.WriteLine($"Number of hotels fetched: {hotels?.Count}");

            ViewBag.Hotels = hotels ?? new List<HotelDto>();

            return View("AddaFood", new FoodDto());
        }
      




        [HttpPost]
            public async Task<IActionResult> AddFoods([FromForm] FoodDto _food)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Hotels =await _db.Hotels
                        .Select(h => new HotelDto { HotelId = h.HotelId, HotelName = h.HotelName })
                        .ToListAsync();

                    return View("AddaFood", _food); 
                }

                var food = new Food
                {
                    FoodName = _food.FoodName,
                    Description = _food.Description,
                    Category = _food.Category,
                    Discount = _food.Discount,
                    Cost = _food.Cost,
                    Amount = _food.Amount,
                    HotelId = _food.HotelId,                  
                };

               await _db.Foods.AddAsync(food);
                await _db.SaveChangesAsync();
            List<FoodDto> _ListofFoods =await  _db.Foods.Select(f => new FoodDto
            {
                FoodId = f.FoodId,
                FoodName = f.FoodName,
                Description = f.Description,
                Cost = f.Cost,
                Amount = f.Amount,
                Category = f.Category,
                Discount = f.Discount,
                HotelId = f.HotelId,
                HotelName = f.Hotel.HotelName
            }).ToListAsync();

            return View("ListOfFoods",_ListofFoods); 
            }

        [HttpPost]
        public async Task<IActionResult> DeleteFoods([FromForm] int id)
        {
            var _foodobj = await _db.Foods.FindAsync(id);
            if (_foodobj == null)
            {
                return NotFound();
            }

            _db.Foods.Remove(_foodobj);
            await _db.SaveChangesAsync();


            List<FoodDto> _foodList=await _db.Foods.Select(f=> new FoodDto
            {
                FoodId = f.FoodId,
                FoodName = f.FoodName,
                Description = f.Description,
                Cost = f.Cost,
                Amount = f.Amount,
                Category = f.Category,
                Discount = f.Discount,
                HotelId = f.HotelId,
                HotelName = f.Hotel.HotelName
            }).ToListAsync();


            return View("ListOfFoods", _foodList);
            
        }

    }
}
