﻿// <auto-generated />
using System;
using Kiper.Condominio.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kiper.Condominio.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190808032035_UpdateResidentTable")]
    partial class UpdateResidentTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kiper.Condominio.Domain.Entities.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Block")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("CondominiumId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(4964));

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(6233));

                    b.Property<Guid>("ModifiedBy");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Roof")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CondominiumId");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("Kiper.Condominio.Domain.Entities.Condominium", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 8, 0, 20, 35, 100, DateTimeKind.Local).AddTicks(4786));

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 8, 0, 20, 35, 107, DateTimeKind.Local).AddTicks(6927));

                    b.Property<Guid>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Condominium");
                });

            modelBuilder.Entity("Kiper.Condominio.Domain.Entities.Resident", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApartmentId");

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(6029));

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(7292));

                    b.Property<Guid>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Resident");
                });

            modelBuilder.Entity("Kiper.Condominio.Domain.Entities.Apartment", b =>
                {
                    b.HasOne("Kiper.Condominio.Domain.Entities.Condominium", "Condominium")
                        .WithMany("Apartments")
                        .HasForeignKey("CondominiumId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Kiper.Condominio.Domain.Entities.Condominium", b =>
                {
                    b.OwnsOne("Kiper.Condominio.Core.Helpers.ValueObjects.AddressInfo", "Address", b1 =>
                        {
                            b1.Property<Guid>("CondominiumId");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnName("City")
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Complement")
                                .HasColumnName("Complement")
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Neighbourhood")
                                .IsRequired()
                                .HasColumnName("Neighbourhood")
                                .HasColumnType("varchar(150)");

                            b1.Property<int>("Number")
                                .HasColumnName("Number")
                                .HasColumnType("int");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnName("State")
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnName("Street")
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnName("ZipCode")
                                .HasColumnType("varchar(15)");

                            b1.HasKey("CondominiumId");

                            b1.ToTable("Condominium");

                            b1.HasOne("Kiper.Condominio.Domain.Entities.Condominium")
                                .WithOne("Address")
                                .HasForeignKey("Kiper.Condominio.Core.Helpers.ValueObjects.AddressInfo", "CondominiumId")
                                .OnDelete(DeleteBehavior.Restrict);
                        });
                });

            modelBuilder.Entity("Kiper.Condominio.Domain.Entities.Resident", b =>
                {
                    b.HasOne("Kiper.Condominio.Domain.Entities.Apartment", "Apartment")
                        .WithMany("Residents")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("Kiper.Condominio.Core.Helpers.ValueObjects.ContactInfo", "Contact", b1 =>
                        {
                            b1.Property<Guid>("ResidentId");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnName("PhoneNumber")
                                .HasColumnType("varchar(25)");

                            b1.HasKey("ResidentId");

                            b1.ToTable("Resident");

                            b1.HasOne("Kiper.Condominio.Domain.Entities.Resident")
                                .WithOne("Contact")
                                .HasForeignKey("Kiper.Condominio.Core.Helpers.ValueObjects.ContactInfo", "ResidentId")
                                .OnDelete(DeleteBehavior.Restrict);
                        });

                    b.OwnsOne("Kiper.Condominio.Core.Helpers.ValueObjects.DocumentInfo", "Document", b1 =>
                        {
                            b1.Property<Guid>("ResidentId");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnName("Number")
                                .HasColumnType("varchar(11)");

                            b1.HasKey("ResidentId");

                            b1.ToTable("Resident");

                            b1.HasOne("Kiper.Condominio.Domain.Entities.Resident")
                                .WithOne("Document")
                                .HasForeignKey("Kiper.Condominio.Core.Helpers.ValueObjects.DocumentInfo", "ResidentId")
                                .OnDelete(DeleteBehavior.Restrict);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
