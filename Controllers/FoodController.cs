using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Controllers
{
    public class FoodController : Controller
    {
        private readonly FoodContext _db;
        private readonly IFoodService _foodService;
        public FoodController(FoodContext db,IFoodService foodService)
        {
            _db = db;
            _foodService = foodService;
        }

        public async Task<IActionResult> ListOfFoods()
        {
            var FoodLists = await _foodService.GetAllFood();
            if (FoodLists == null)
            {
                return BadRequest();
            }

            return View(FoodLists);
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


            ViewBag.Hotels = hotels ?? new List<HotelDto>();

            return View("AddaFood", new FoodDto());
        }
      




        [HttpPost]
            public async Task<IActionResult> AddFoods([FromForm] FoodDto food)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Hotels =await _db.Hotels
                        .Select(h => new HotelDto { HotelId = h.HotelId, HotelName = h.HotelName })
                        .ToListAsync();

                    return View("AddaFood", food); 
                }
            var result = await _foodService.AddNewFood(food);
            if (!result)
            {
                return BadRequest();
            }

            var ListofFoods = await _foodService.GetAllFood();


            return View("ListOfFoods",ListofFoods); 
            }

        [HttpPost]
        public async Task<IActionResult> DeleteFoods([FromForm] int id)
        {
            var result = await _foodService.DeleteFood(id);
            if(!result){
                return BadRequest();
            }
            var foodList = await _foodService.GetAllFood();

            return View("ListOfFoods", foodList);
            
        }

    }
}
