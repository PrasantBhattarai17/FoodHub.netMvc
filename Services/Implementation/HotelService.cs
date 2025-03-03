using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class HotelService : IHotelService
    {

        private readonly FoodContext _db;

        public HotelService(FoodContext db)
        {
            _db = db;
        }
        public async Task<List<HotelDto>> GetAllHotels()
        {
            var HotelLists=await _db.Hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Location = h.Location,
                HotelName = h.HotelName,
                HotelImg = h.HotelImg,
                category = h.category,
                Cuisines = h.Cuisines,
                Discount = h.Discount,
                Rating = h.Rating,
                Price = h.Price
            }).ToListAsync();
            return HotelLists;
        }

        public async Task<bool> AddHotel(Hotel hotel)
        {
            if (hotel == null)
            {
                return false;
            }

            await _db.Hotels.AddAsync(hotel);
            await _db.SaveChangesAsync();

            return true;
        }



        public async Task<HotelDto> GetHotelById(int id)
        {
            var hotel = await _db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return null;
            }
            var thatHotel = new HotelDto
            {
                HotelId = hotel.HotelId,
                Location = hotel.Location,
                HotelName = hotel.HotelName,
                HotelImg = hotel.HotelImg,
                category = hotel.category,
                Cuisines = hotel.Cuisines,
                Discount = hotel.Discount,
                Rating = hotel.Rating,
                Price = hotel.Price
            };

            return thatHotel;
        }

        public async Task<bool> RemoveHotel(int id)
        {
            var hotel = await _db.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return false;
            }

            _db.Remove(hotel);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateHotel(HotelDto hotel)
        {
            var existingHotel = await _db.Hotels.FindAsync(hotel.HotelId);

            if (existingHotel == null)
            {
                return false;
            }
            existingHotel.HotelName =hotel.HotelName;
            existingHotel.HotelImg = hotel.HotelImg;
            existingHotel.category = hotel.category;
            existingHotel.Cuisines = hotel.Cuisines;
            existingHotel.Discount = hotel.Discount;
            existingHotel.Price = hotel.Price;
            existingHotel.Location = hotel.Location;
            existingHotel.Rating = hotel.Rating;


            await _db.SaveChangesAsync();
            return true;
        }
    }
}
