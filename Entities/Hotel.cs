using System.ComponentModel.DataAnnotations;

namespace YetiMunch.Entities
{
    public class Hotel
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

        public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}
