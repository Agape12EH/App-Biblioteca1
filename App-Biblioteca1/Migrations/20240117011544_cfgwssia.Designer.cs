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
    [Migration("20240117011544_cfgwssia")]
    partial class cfgwssia
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App_Biblioteca1.Models.BookStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateStored")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuantityTotal")
                        .HasColumnType("int");

                    b.Property<string>("isbnBook")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookStore");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Books", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("AgePublication")
                        .HasColumnType("date");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Delete")
                        .HasColumnType("tinyint");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LoanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.HasIndex("StoreId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CurrentReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpectedReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoanState")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<Guid?>("BooksId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Registrationdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TakenActions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BooksId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("App_Biblioteca1.Models.Books", b =>
                {
                    b.HasOne("App_Biblioteca1.Models.Loan", null)
                        .WithMany("Books")
                        .HasForeignKey("LoanId");

                    b.HasOne("App_Biblioteca1.Models.BookStore", "Store")
                        .WithMany("Books")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
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
                    b.HasOne("App_Biblioteca1.Models.Books", "Books")
                        .WithMany("StateBooks")
                        .HasForeignKey("BooksId");

                    b.HasOne("App_Biblioteca1.Models.User", "User")
                        .WithMany("stateBooks")
                        .HasForeignKey("UserId");

                    b.Navigation("Books");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.BookStore", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Books", b =>
                {
                    b.Navigation("StateBooks");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.Loan", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("App_Biblioteca1.Models.User", b =>
                {
                    b.Navigation("Loans");

                    b.Navigation("stateBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
