using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Repository.IRepository;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class ShopService : IShopService
    {
        private readonly IRepository<Hotel> _shopRepository;

        public ShopService(IRepository<Hotel> shopRepository)
        {
            _shopRepository = shopRepository;
                 }

        public async Task<HotelDto> ViewHotelDetails(int id)
        {

            var hotel = await _shopRepository.GetQueryable().Include(f => f.Foods).FirstOrDefaultAsync(h=>h.HotelId==id);
               
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

            return hotelDetails;

        }
    }
}
