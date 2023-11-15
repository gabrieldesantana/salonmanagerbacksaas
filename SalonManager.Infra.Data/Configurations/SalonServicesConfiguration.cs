using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Configurations
{
    public class SalonServicesConfiguration : IEntityTypeConfiguration<SalonService>
    {
        public void Configure(EntityTypeBuilder<SalonService> builder)
        {
            builder.ToTable("SalonServices");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.Category).HasColumnType("varchar(40)").IsRequired();
            builder.Property(p => p.Price).HasColumnType("real");

            builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp");
        }

    }
}
