using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YetiMunch.Entities;

namespace YetiMunch.Infrastructure.Data.Configurations
{
    public class FoodConfiguration:IEntityTypeConfiguration<Food>
    {
       public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasOne(f => f.Hotel)
                  .WithMany(h => h.Foods)
                  .HasForeignKey(f => f.HotelId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
