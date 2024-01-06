﻿// <auto-generated />
using System;
using App_Biblioteca1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App_Biblioteca1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240105224348_Tables")]
    partial class Tables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App_Biblioteca1.Models.Books", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("AgePublication")
                        .HasColumnType("date");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuantityAvailable")
                        .HasColumnType("int");

                    b.Property<int>("QuantityTotal")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateInventory")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActualReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpectedReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoanState")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("guidBook")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("guidsBooks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.StateBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Registrationdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TakenActions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserActorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserActorId");

                    b.ToTable("StateBooks");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BooksLoan", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LoansId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BooksId", "LoansId");

                    b.HasIndex("LoansId");

                    b.ToTable("BooksLoan");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Books", b =>
                {
                    b.HasOne("App_Biblioteca1.Models.Inventory", "Inventory")
                        .WithMany("Books")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Loan", b =>
                {
                    b.HasOne("App_Biblioteca1.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.StateBook", b =>
                {
                    b.HasOne("App_Biblioteca1.Models.Books", "Book")
                        .WithMany("StateBook")
                        .HasForeignKey("BookId");

                    b.HasOne("App_Biblioteca1.Models.User", "UserActor")
                        .WithMany()
                        .HasForeignKey("UserActorId");

                    b.Navigation("Book");

                    b.Navigation("UserActor");
                });

            modelBuilder.Entity("BooksLoan", b =>
                {
                    b.HasOne("App_Biblioteca1.Models.Books", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Biblioteca1.Models.Loan", null)
                        .WithMany()
                        .HasForeignKey("LoansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Books", b =>
                {
                    b.Navigation("StateBook");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Inventory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
