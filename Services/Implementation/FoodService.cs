using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Migrations;
using YetiMunch.Models;
using YetiMunch.Repository.IRepository;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class FoodService : IFoodService
    {
        private readonly IRepository<Food> _foodRepository;

        public FoodService(IRepository<Food> foodRepository)
        {
            _foodRepository = foodRepository;
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
            await _foodRepository.Add(food);
            return true;

        }

        public async Task<List<FoodDto>> GetAllFood()
        {

            var foodlist = await _foodRepository.GetQueryable().Include(F => F.Hotel).ToListAsync();
            var food = foodlist.Select(f => new FoodDto
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

            if (food == null)
            {
                return null;
            }

            return food;

        }
        public async Task<FoodDto> GetFoodById(int id)
        {
            var food = await _foodRepository.GetById(id);
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
            var food =await  _foodRepository.GetById(id);
            if (food == null)
            {
                return false;
            }
             await _foodRepository.Delete(food.FoodId);
            return true; 
        }


        

        }
    }

