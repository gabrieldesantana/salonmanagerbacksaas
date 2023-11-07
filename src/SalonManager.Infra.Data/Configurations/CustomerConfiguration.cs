using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Cpf).HasColumnType("varchar(14)").IsRequired();
            builder.Property(p => p.Name).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.Nickname).HasColumnType("varchar(30)").IsRequired();
            builder.Property(p => p.Gender).HasColumnType("varchar(10)").IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();

            builder.Property(p => p.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("timestamp");


            builder.Property(p => p.LastService).HasColumnType("text").IsRequired();

            builder.Property(p => p.LastServiceDate)
            .HasColumnName("LastServiceDate")
            .HasColumnType("timestamp");

            builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp");


            builder.HasMany(c => c.Appointments)
            .WithOne(a => a.CustomerAppointment)
            .HasForeignKey(a => a.CustomerAppointmentId);

            builder.HasIndex(i => i.PhoneNumber).HasName("idx_customer_phonenumber"); //Criar um indice
        }

    }
}