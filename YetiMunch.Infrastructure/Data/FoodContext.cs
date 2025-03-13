using Microsoft.EntityFrameworkCore;
using YetiMunch.Entities;
using YetiMunch.Infrastructure.Data.Configurations;

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
            modelBuilder.ApplyConfiguration(new FoodConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
