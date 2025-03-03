using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Migrations;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class FoodService : IFoodService
    {
        private readonly FoodContext _db;
        public FoodService(FoodContext db)
        {
            _db = db;
        }
        public async Task<bool> AddNewFood(FoodDto foodDto)
        {
            if (foodDto == null)
            {
                return false;
            }
            var food = new Food
            {
                FoodName = foodDto.FoodName,
                Description = foodDto.Description,
                Category = foodDto.Category,
                Discount = foodDto.Discount,
                FoodImgUrl=foodDto.FoodImgUrl,
                Cost = foodDto.Cost,
                Amount = foodDto.Amount,
                HotelId = foodDto.HotelId,
            };
            await _db.Foods.AddAsync(food);
            await _db.SaveChangesAsync();

            return true;

        }

        public async Task<List<FoodDto>> GetAllFood()
        {
            var food = await _db.Foods.Select(f => new FoodDto
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

            if (food == null)
            {
                return null;
            }

            return food;

        }
        public async Task<FoodDto> GetFoodById(int id)
        {
            var food = await _db.Foods.FindAsync(id);
            if (food == null)
            {
                return null;
            }
            FoodDto foodDto = new FoodDto
            {
                FoodId = food.FoodId,
                FoodName = food.FoodName,
                Description = food.Description,
                Cost = food.Cost,
                Amount = food.Amount,
                Category = food.Category,
                Discount = food.Discount,
                HotelId = food.HotelId,
                HotelName = food.Hotel.HotelName
            };
            return foodDto;            
        }

        public async Task<bool> DeleteFood(int id)
        {
            var food =await  _db.Foods.FindAsync(id);
            if (food == null)
            {
                return false;
            }
            _db.Foods.Remove(food);
            await _db.SaveChangesAsync();
            return true; 
        }


        

        }
    }

