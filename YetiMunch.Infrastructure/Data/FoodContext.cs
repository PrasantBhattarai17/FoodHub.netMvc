using Microsoft.EntityFrameworkCore;
using YetiMunch.Entities;

namespace YetiMunch.Data
{
    public class FoodContext:DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options):base(options) { }
       
        public DbSet<User> Users { get; set; }

        public DbSet<Food> Foods { get; set; }


        public DbSet<Hotel> Hotels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .HasOne(f => f.Hotel) //must have one Hotel
                .WithMany(h => h.Foods) //One Hotel can have many foods
                .HasForeignKey(f => f.HotelId)//HotelId is Foreign key in Food Table
                .OnDelete(DeleteBehavior.Cascade);//When the hotels are deleted it automatically deletes the assocaited foods too

            base.OnModelCreating(modelBuilder);

        }

        
    }
}
