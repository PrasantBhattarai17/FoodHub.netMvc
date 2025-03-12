using Microsoft.EntityFrameworkCore;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
                 }

        public async Task<HotelDto> ViewHotelDetails(int id)
        {

            var hotel = await _unitOfWork.Repository<Hotel>().GetQueryable().Include(f => f.Foods).FirstOrDefaultAsync(h=>h.HotelId==id);
               
            if (hotel == null)
            {
                return null;
            }

            HotelDto hotelDetails = new HotelDto
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                HotelImg = hotel.HotelImg,
                category = hotel.category,
                Cuisines = hotel.Cuisines,
                Discount = hotel.Discount,
                Price = hotel.Price,
                Location = hotel.Location,
                Rating = hotel.Rating,
                Foods = hotel.Foods.Select(f => new FoodDto
                {
                    FoodId = f.FoodId,
                    FoodName = f.FoodName,
                    Description = f.Description,
                    Cost = f.Cost,
                    Amount = f.Amount,
                    Category = f.Category,
                    Discount = f.Discount,
                    FoodImgUrl = f.FoodImgUrl,
                }).ToList()
            };

            if (hotelDetails == null)
            {
                return null;
            }
            Console.WriteLine("Error",hotelDetails);
            return hotelDetails;

        }
    }
}
