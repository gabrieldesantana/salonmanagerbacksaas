using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure (EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.CNPJ).HasColumnType("varchar(18)").IsRequired();

            builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp");

            builder.HasMany(c => c.Employees)
           .WithOne(a => a.Company)
           .HasForeignKey(a => a.CompanyId);
        }
    }
}
