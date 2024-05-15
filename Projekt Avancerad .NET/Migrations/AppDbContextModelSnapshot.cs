﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt_Avancerad_.NET.Data;

#nullable disable

namespace Projekt_Avancerad_.NET.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("AppointmentId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            AppointmentId = 1,
                            CompanyId = 1,
                            CustomerId = 1,
                            Date = new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 2,
                            CompanyId = 2,
                            CustomerId = 1,
                            Date = new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 3,
                            CompanyId = 2,
                            CustomerId = 2,
                            Date = new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Models.Models.AppointmentHistory", b =>
                {
                    b.Property<int>("AppointmentHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentHistoryId"));

                    b.Property<string>("AppointmentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChangeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Changes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OriginalAppointmentAppointmentId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentHistoryId");

                    b.HasIndex("OriginalAppointmentAppointmentId");

                    b.ToTable("AppointmentHistories");
                });

            modelBuilder.Entity("Models.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            Name = "Ringhals"
                        },
                        new
                        {
                            CompanyId = 2,
                            Name = "Derome"
                        });
                });

            modelBuilder.Entity("Models.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "simon.nilsson.10@gmail.com",
                            Name = "Simon Nilsson",
                            Phone = "0702611037"
                        },
                        new
                        {
                            CustomerId = 2,
                            Email = "antonnilsson@gmail.com",
                            Name = "Anton Nilsson",
                            Phone = "0481771495"
                        },
                        new
                        {
                            CustomerId = 3,
                            Email = "sodergrenemm@gmail.com",
                            Name = "Emm Södergren",
                            Phone = "0730351076"
                        });
                });

            modelBuilder.Entity("Models.Models.LoginInfo", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("LoginInfos");

                    b.HasData(
                        new
                        {
                            LoginId = 1,
                            EMail = "admin@admin.se",
                            Password = "1234",
                            Role = "admin"
                        },
                        new
                        {
                            LoginId = 2,
                            CustomerId = 1,
                            EMail = "simon.nilsson.10@gmail.com",
                            Password = "1234",
                            Role = "customer"
                        },
                        new
                        {
                            LoginId = 3,
                            CustomerId = 3,
                            EMail = "sodergrenemm@gmail.com",
                            Password = "1234",
                            Role = "customer"
                        },
                        new
                        {
                            LoginId = 4,
                            CustomerId = 2,
                            EMail = "antonnilsson@gmail.com",
                            Password = "1234",
                            Role = "customer"
                        },
                        new
                        {
                            LoginId = 5,
                            CompanyId = 1,
                            EMail = "ringhals@varberg.se",
                            Password = "1234",
                            Role = "company"
                        },
                        new
                        {
                            LoginId = 6,
                            CompanyId = 2,
                            EMail = "derome@varberg.se",
                            Password = "1234",
                            Role = "company"
                        });
                });

            modelBuilder.Entity("Models.Models.Appointment", b =>
                {
                    b.HasOne("Models.Models.Company", "Company")
                        .WithMany("Appointments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Models.Models.AppointmentHistory", b =>
                {
                    b.HasOne("Models.Models.Appointment", "OriginalAppointment")
                        .WithMany()
                        .HasForeignKey("OriginalAppointmentAppointmentId");

                    b.Navigation("OriginalAppointment");
                });

            modelBuilder.Entity("Models.Models.LoginInfo", b =>
                {
                    b.HasOne("Models.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Models.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Company");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Models.Models.Company", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Models.Models.Customer", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}