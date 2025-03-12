using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Repository.IRepository;

namespace YetiMunch.Repository.Implementation
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {

        private readonly FoodContext _db;
        public AuthRepository(FoodContext db) : base(db)
        {
            _db = db;
        }
       


        public async Task<List<HotelDto>> GetHotels()
        {

            return await _db.Hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                HotelImg = h.HotelImg,
                category = h.category,
                Cuisines = h.Cuisines,
                Discount = h.Discount,
                Price = h.Price,
                Location = h.Location,
                Rating = h.Rating
            }).ToListAsync();
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);

        }

        public async Task<bool> IsUserExists(string username, string email)
        {

            return await _db.Users.AnyAsync(u => u.Username == username || u.Email  == email);
        }
    }
}
