using System.ComponentModel.DataAnnotations;
using YetiMunch.Entities;

namespace YetiMunch.Models
{
    public class HotelDto
    {

        [Key]
        public int HotelId { get; set; }

        [Required]
        public string HotelName { get; set; }

        public int Rating { get; set; }

        public string Price { get; set; }

        public string Location { get; set; }

        public string Cuisines { get; set; }
        public string HotelImg { get; set; }

        public string category { get; set; }

        public string Discount { get; set; }

        public List<FoodDto> Foods { get; set; } = new List<FoodDto>();

        public static implicit operator List<object>(HotelDto v)
        {
            throw new NotImplementedException();
        }
    }
}

