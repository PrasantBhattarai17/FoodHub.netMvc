using System.ComponentModel.DataAnnotations;
using YetiMunch.Entities;

namespace YetiMunch.Models
{
    public class FoodDto
    {
        [Key]
        public int FoodId { get; set; }

        public string FoodName { get; set; }

        public string Description { get; set; } //Description about foood short

        public int Cost { get; set; }

        public string Category { get; set; } //Veg nonveg vegan ..dairy..beverage ..

        public string FoodImgUrl { get; set; }
        public string Amount { get; set; } //amount of the food 

        public string Discount { get; set; }//discounts if available

        [Required(ErrorMessage ="Hotel is Necessary")]
        public int HotelId { get; set; }
        public string HotelName { get; set; }

    }
}
