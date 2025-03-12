using YetiMunch.Entities;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

namespace YetiMunch.Services.Implementation
{
    public class HotelService : IHotelService
    {

        private readonly IUnitOfWork _unitOfWork;

        public HotelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HotelDto>> GetAllHotels()
        {
            var hotelLists = await _unitOfWork.Repository<Hotel>().GetAll();

            var hotelListsDTO=hotelLists.Select(h => new HotelDto
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
            }).ToList();
            return hotelListsDTO;
        }

        public async Task<bool> AddHotel(Hotel hotel)
        {
            using var Transaction=await _unitOfWork.BeginTransaction();
            {
          try { 
            if (hotel == null)
            {
                return false;
            }

            await _unitOfWork.Repository<Hotel>().Add(hotel);
           await  _unitOfWork.SaveAsync();
           await Transaction.CommitAsync();
            return true;
           }
          catch (Exception)
                {
                    await Transaction.RollbackAsync();
                    throw;
                }
          }
            
        }



        public async Task<HotelDto> GetHotelById(int id)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetById(id);
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
            var hotel = await _unitOfWork.Repository<Hotel>().GetById(id);

            if (hotel == null)
            {
                return false;
            }

            await _unitOfWork.Repository<Hotel>().Delete(hotel.HotelId);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> UpdateHotel(HotelDto hotel)
        {
            using var transaction = await _unitOfWork.BeginTransaction();
            {
          try {
            var existingHotel = await _unitOfWork.Repository<Hotel>().GetById(hotel.HotelId);

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

            await _unitOfWork.Repository<Hotel>().Update(existingHotel);

            await _unitOfWork.SaveAsync();
             await transaction.CommitAsync();

            return true;
                }
         catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
             }
         }
        }
    }
}
