using YetiMunch.Entities;
using YetiMunch.Models;

namespace YetiMunch.Services.Interfaces
{
    public interface IFoodService
    {
        Task<List<FoodDto>> GetAllFood();
        Task<bool> AddNewFood(FoodDto food);
        Task<bool> DeleteFood(int id);
        Task<FoodDto> GetFoodById(int id);


    }
}
