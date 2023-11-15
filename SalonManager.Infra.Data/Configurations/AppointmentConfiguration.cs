using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(p => p.Id);


            builder.Property(e => e.CustomerAppointmentId)
           .HasColumnName("CustomerAppointmentId")
           .HasColumnType("int");

            builder.Property(e => e.ServiceAppointmentId)
            .HasColumnName("ServiceAppointmentId")
            .HasColumnType("int");


            builder.Property(p => p.Date)
            .HasColumnName("Date")
            .HasColumnType("timestamp");

            builder.Property(p => p.FinishedDate)
            .HasColumnName("FinishedDate")
            .HasColumnType("timestamp");

            builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp");

            builder.Property(p => p.Status).HasConversion<string>();

            builder.Property(p => p.Description).HasColumnType("varchar(150)").IsRequired();

            builder.Property(p => p.PaymentWay).HasColumnType("varchar(50)");
            builder.Property(p => p.PaymentMethod).HasColumnType("varchar(50)");

            builder.Property(p => p.Value).HasColumnType("real");

            builder.Property(p => p.Finished)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(p => p.CustomerAppointment)
            .WithOne()
            .HasForeignKey<Customer>(e => e.Id);

            //builder.HasOne(p => p.ServiceAppointment)
            //.WithOne()
            //.HasForeignKey<SalonService>(e => e.Id);
        }

    }
}