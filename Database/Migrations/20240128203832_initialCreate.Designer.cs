﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240128203832_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("CategoryPromoCode", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("PromoCodesId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "PromoCodesId");

                    b.HasIndex("PromoCodesId");

                    b.ToTable("CategoryPromoCode");
                });

            modelBuilder.Entity("ColorProduct", b =>
                {
                    b.Property<int>("ColorsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("ColorsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ColorProduct");
                });

            modelBuilder.Entity("Database.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Database.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("longblob");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Database.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Hex")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Discount_amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EstimatedDeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("HasBeenPaid")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParcelTracking")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total_without_tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrentStock")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Ean")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDeactivated")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceWithoutTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Database.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("Database.Entities.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalWithoutTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductOrder");
                });

            modelBuilder.Entity("Database.Entities.PromoCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("HomeVisible")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PromoCode");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeactivated")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Database.Entities.UserCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCart");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("Database.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryPromoCode", b =>
                {
                    b.HasOne("Database.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.PromoCode", null)
                        .WithMany()
                        .HasForeignKey("PromoCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ColorProduct", b =>
                {
                    b.HasOne("Database.Entities.Color", null)
                        .WithMany()
                        .HasForeignKey("ColorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.Address", b =>
                {
                    b.HasOne("Database.Entities.User", null)
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.Product", b =>
                {
                    b.HasOne("Database.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Database.Entities.ProductImage", b =>
                {
                    b.HasOne("Database.Entities.Product", null)
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.ProductOrder", b =>
                {
                    b.HasOne("Database.Entities.Order", null)
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Database.Entities.UserCart", b =>
                {
                    b.HasOne("Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.User", null)
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Database.Entities.Order", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("Database.Entities.Product", b =>
                {
                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Carts");
                });
#pragma warning restore 612, 618
        }
    }
}
