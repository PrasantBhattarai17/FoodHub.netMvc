using System.ComponentModel.DataAnnotations;

namespace YetiMunch.Entities
{
    public class Food
    {
        [Key]
    public int FoodId { get; set; }

    public string FoodName { get; set; }

    public string Description { get; set; } //Description about foood short

    public int Cost { get; set; } 

    public string Category { get; set; } //Veg nonveg vegan ..dairy..beverage ..

    public string Amount { get; set; } //amount of the food 

     public string FoodImgUrl { get; set; }//img of the food

    public string Discount { get; set; }//discounts if available

    public int HotelId { get; set; } //foreign Key

    public virtual  Hotel Hotel { get; set; } //Navigation Property


    }
}
