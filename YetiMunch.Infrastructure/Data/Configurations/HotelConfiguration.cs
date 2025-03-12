using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using YetiMunch.Entities;

namespace YetiMunch.Infrastructure.Data.Configurations
{
    class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.HotelId);
            builder.Property(h => h.HotelName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(h => h.Location)
                .IsRequired();
        }
    }
}
