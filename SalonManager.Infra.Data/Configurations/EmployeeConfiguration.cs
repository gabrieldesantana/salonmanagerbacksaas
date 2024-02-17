using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Cpf).HasColumnType("varchar(14)").IsRequired();
            builder.Property(p => p.Name).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.Nickname).HasColumnType("varchar(30)").IsRequired();
            builder.Property(p => p.Gender).HasColumnType("varchar(10)").IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();

            builder.Property(p => p.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("timestamp");

            builder.Property(e => e.UserId)
            .HasColumnName("UserId")
            .HasColumnType("int");

            builder.Property(e => e.CompanyId)
            .HasColumnName("CompanyId")
            .HasColumnType("int");

            builder.HasMany(c => c.Appointments)
            .WithOne(a => a.EmployeeAppointment)
            .HasForeignKey(a => a.EmployeeAppointmentId);
        }

    }
}