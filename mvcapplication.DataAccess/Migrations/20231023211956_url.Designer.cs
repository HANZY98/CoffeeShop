﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mvcapplication.DataAccess.Data;

#nullable disable

namespace mvcapplication.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231023211956_url")]
    partial class url
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("mvcapplication.Models.Category", b =>
                {
                    b.Property<int>("Category_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Category_Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Category_Id = 1,
                            DisplayOrder = 1,
                            JobTitle = "GM",
                            Name = "Action"
                        },
                        new
                        {
                            Category_Id = 2,
                            DisplayOrder = 2,
                            JobTitle = "RM",
                            Name = "Adventure"
                        },
                        new
                        {
                            Category_Id = 3,
                            DisplayOrder = 3,
                            JobTitle = "GM",
                            Name = "Comedy"
                        },
                        new
                        {
                            Category_Id = 4,
                            DisplayOrder = 4,
                            JobTitle = "RM",
                            Name = "Drama"
                        });
                });

            modelBuilder.Entity("mvcapplication.Models.Product", b =>
                {
                    b.Property<int>("Product_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Product_Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Product_Id = 1,
                            Author = "Bean",
                            Category_Id = 1,
                            Description = "This film is classic",
                            ISBN = "",
                            ImageURL = "",
                            Price = 20.0,
                            Price100 = 200.0,
                            Price50 = 100.0,
                            Title = "MR Bean 1"
                        },
                        new
                        {
                            Product_Id = 2,
                            Author = "Bean",
                            Category_Id = 2,
                            Description = "Another classic",
                            ISBN = "",
                            ImageURL = "",
                            Price = 20.0,
                            Price100 = 200.0,
                            Price50 = 100.0,
                            Title = "MR Bean 2"
                        },
                        new
                        {
                            Product_Id = 3,
                            Author = "Bean",
                            Category_Id = 3,
                            Description = "Yet another classic",
                            ISBN = "",
                            ImageURL = "",
                            Price = 20.0,
                            Price100 = 200.0,
                            Price50 = 100.0,
                            Title = "MR Bean 3"
                        },
                        new
                        {
                            Product_Id = 4,
                            Author = "Unknown",
                            Category_Id = 4,
                            Description = "Not related to Mr. Bean",
                            ISBN = "",
                            ImageURL = "",
                            Price = 30.0,
                            Price100 = 300.0,
                            Price50 = 150.0,
                            Title = "Another Movie"
                        });
                });

            modelBuilder.Entity("mvcapplication.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutMe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AboutMe = "I love coding",
                            FirstName = "Luke",
                            JobTitle = "Engineer",
                            Salary = "100,000",
                            SecondName = "Hands"
                        },
                        new
                        {
                            Id = 2,
                            AboutMe = "I love coding",
                            FirstName = "Jason",
                            JobTitle = "Engineer",
                            Salary = "100,000",
                            SecondName = "Hands"
                        },
                        new
                        {
                            Id = 3,
                            AboutMe = "I love coding",
                            FirstName = "Dean",
                            JobTitle = "Engineer",
                            Salary = "100,000",
                            SecondName = "Hands"
                        },
                        new
                        {
                            Id = 4,
                            AboutMe = "I love coding",
                            FirstName = "Duke",
                            JobTitle = "Engineer",
                            Salary = "100,000",
                            SecondName = "Hands"
                        },
                        new
                        {
                            Id = 5,
                            AboutMe = "I love coding",
                            FirstName = "Grham",
                            JobTitle = "Engineer",
                            Salary = "100,000",
                            SecondName = "Hands"
                        });
                });

            modelBuilder.Entity("mvcapplication.Models.Product", b =>
                {
                    b.HasOne("mvcapplication.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
