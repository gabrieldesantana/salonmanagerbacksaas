using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.CompanyAdministrator).IsRequired();
            builder.Property(p => p.Role).HasConversion<string>();
            builder.Property(p => p.Login).HasColumnType("varchar(20)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.Password).IsRequired();

            builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp");
        }

    }
}