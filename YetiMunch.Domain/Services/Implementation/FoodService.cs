using Microsoft.EntityFrameworkCore;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddNewFood(FoodDto foodDto)
        {
            using var transaction = await _unitOfWork.BeginTransaction();
            {
                try
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
            await _unitOfWork.Repository<Food>().Add(food);
            await _unitOfWork.SaveAsync();
            await transaction.CommitAsync();
            return true;
           }
           catch
            {
                    await transaction.RollbackAsync();
                    throw;
             }
            }

        }

        public async Task<List<FoodDto>> GetAllFood()
        {

            var foodlist = await _unitOfWork.Repository<Food>().GetQueryable().Include(F => F.Hotel).ToListAsync();
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
            var food = await _unitOfWork.Repository<Food>().GetById(id);
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
            var food =await  _unitOfWork.Repository<Food>().GetById(id);
            if (food == null)
            {
                return false;
            }
             await _unitOfWork.Repository<Food>().Delete(food.FoodId);
            await _unitOfWork.SaveAsync();
            return true; 
        }


        

        }
    }

