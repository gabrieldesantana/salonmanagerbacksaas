﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SalonManager.Infra.Data.Context;

#nullable disable

namespace SalonManager.Application.Persistence.Migrations
{
    [DbContext(typeof(SalonManagerDbContext))]
    partial class SalonManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SalonManager.Domain.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Actived")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("CreatedAt");

                    b.Property<int>("CustomerAppointmentId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerAppointmentId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp")
                        .HasColumnName("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("Finished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("FinishedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("FinishedDate");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PaymentWay")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ServiceAppointmentId")
                        .HasColumnType("int")
                        .HasColumnName("ServiceAppointmentId");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAppointmentId");

                    b.HasIndex("ServiceAppointmentId");

                    b.ToTable("Appointments", (string)null);
                });

            modelBuilder.Entity("SalonManager.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Actived")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("BirthDate");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("LastService")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastServiceDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("LastServiceDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Times")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .HasDatabaseName("idx_customer_phonenumber");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("SalonManager.Domain.Entities.SalonService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Actived")
                        .HasColumnType("boolean");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SalonServices", (string)null);
                });

            modelBuilder.Entity("SalonManager.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Actived")
                        .HasColumnType("boolean");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SalonManager.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("SalonManager.Domain.Entities.Customer", "CustomerAppointment")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerAppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalonManager.Domain.Entities.SalonService", "ServiceAppointment")
                        .WithMany()
                        .HasForeignKey("ServiceAppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerAppointment");

                    b.Navigation("ServiceAppointment");
                });

            modelBuilder.Entity("SalonManager.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
