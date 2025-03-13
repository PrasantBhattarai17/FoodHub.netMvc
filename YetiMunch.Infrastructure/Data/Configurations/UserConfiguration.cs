
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YetiMunch.Entities;

namespace YetiMunch.Infrastructure.Data.Configurations
{
    public  class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.PasswordH)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            //To-do
            //The orders Entity yet to be implemented in the database so the ramaining mapping between users entity and the 
            //orders entity is yet to be done!!

        }
    }
}
