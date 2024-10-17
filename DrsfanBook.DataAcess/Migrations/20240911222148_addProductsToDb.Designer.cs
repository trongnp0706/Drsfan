﻿// <auto-generated />
using DrsfanBook.DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DrsfanBook.DataAcess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240911222148_addProductsToDb")]
    partial class addProductsToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrsfanBook.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "SciFi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("DrsfanBook.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "F. Scott Fitzgerald",
                            Description = "A novel written by F. Scott Fitzgerald.",
                            ISBN = "978-0743273565",
                            ListPrice = 10.99,
                            Price = 9.9900000000000002,
                            Price100 = 7.9900000000000002,
                            Price50 = 8.9900000000000002,
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Harper Lee",
                            Description = "A novel by Harper Lee published in 1960.",
                            ISBN = "978-0061120084",
                            ListPrice = 14.99,
                            Price = 13.99,
                            Price100 = 11.99,
                            Price50 = 12.99,
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            Id = 3,
                            Author = "George Orwell",
                            Description = "A dystopian novel by George Orwell.",
                            ISBN = "978-0451524935",
                            ListPrice = 9.9900000000000002,
                            Price = 8.9900000000000002,
                            Price100 = 6.9900000000000002,
                            Price50 = 7.9900000000000002,
                            Title = "1984"
                        },
                        new
                        {
                            Id = 4,
                            Author = "J.D. Salinger",
                            Description = "A novel by J.D. Salinger.",
                            ISBN = "978-0316769488",
                            ListPrice = 8.9900000000000002,
                            Price = 7.9900000000000002,
                            Price100 = 5.9900000000000002,
                            Price50 = 6.9900000000000002,
                            Title = "The Catcher in the Rye"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Jane Austen",
                            Description = "A romantic novel by Jane Austen.",
                            ISBN = "978-1503290563",
                            ListPrice = 11.99,
                            Price = 10.99,
                            Price100 = 8.9900000000000002,
                            Price50 = 9.9900000000000002,
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = 6,
                            Author = "J.R.R. Tolkien",
                            Description = "A fantasy novel by J.R.R. Tolkien.",
                            ISBN = "978-0547928227",
                            ListPrice = 12.99,
                            Price = 11.99,
                            Price100 = 9.9900000000000002,
                            Price50 = 10.99,
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 7,
                            Author = "Herman Melville",
                            Description = "A novel by Herman Melville.",
                            ISBN = "978-1503280786",
                            ListPrice = 13.99,
                            Price = 12.99,
                            Price100 = 10.99,
                            Price50 = 11.99,
                            Title = "Moby Dick"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
