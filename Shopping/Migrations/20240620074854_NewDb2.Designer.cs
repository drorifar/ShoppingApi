﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shopping.Context;

#nullable disable

namespace Shopping.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20240620074854_NewDb2")]
    partial class NewDb2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shopping.Models.Entities.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "Soft drinks, coffees, teas, beers, and ales",
                            Name = "Beverages"
                        },
                        new
                        {
                            ID = 2,
                            Description = "Sweet and savory sauces, relishes, spreads, and seasonings",
                            Name = "Condiments"
                        },
                        new
                        {
                            ID = 3,
                            Description = "Milk, cheese, yogurt, and butter",
                            Name = "Dairy"
                        },
                        new
                        {
                            ID = 4,
                            Description = "Breads, pastries, and cakes",
                            Name = "Bakery"
                        },
                        new
                        {
                            ID = 5,
                            Description = "Fruits and vegetables",
                            Name = "Produce"
                        },
                        new
                        {
                            ID = 6,
                            Description = "Beef, chicken, pork, and seafood",
                            Name = "Meat"
                        },
                        new
                        {
                            ID = 7,
                            Description = "Frozen meals, ice cream, and frozen vegetables",
                            Name = "Frozen Foods"
                        });
                });

            modelBuilder.Entity("Shopping.Models.Entities.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CategoryID = 1,
                            Description = "10 boxes x 20 bags",
                            Name = "Chai",
                            Price = 10f
                        },
                        new
                        {
                            ID = 2,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Chang",
                            Price = 20f
                        },
                        new
                        {
                            ID = 3,
                            CategoryID = 1,
                            Description = "12 - 355 ml cans",
                            Name = "Guaraná Fantástica",
                            Price = 30f
                        },
                        new
                        {
                            ID = 4,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Sasquatch Ale",
                            Price = 40f
                        },
                        new
                        {
                            ID = 5,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Steeleye Stout",
                            Price = 50f
                        },
                        new
                        {
                            ID = 6,
                            CategoryID = 2,
                            Description = "12 - 550 ml bottles",
                            Name = "Aniseed Syrup",
                            Price = 60f
                        },
                        new
                        {
                            ID = 7,
                            CategoryID = 2,
                            Description = "48 - 6 oz jars",
                            Name = "Chef Anton's Cajun Seasoning",
                            Price = 70f
                        },
                        new
                        {
                            ID = 8,
                            CategoryID = 2,
                            Description = "36 boxes",
                            Name = "Chef Anton's Gumbo Mix",
                            Price = 80f
                        },
                        new
                        {
                            ID = 9,
                            CategoryID = 2,
                            Description = "12 - 8 oz jars",
                            Name = "Grandma's Boysenberry Spread",
                            Price = 90f
                        },
                        new
                        {
                            ID = 10,
                            CategoryID = 2,
                            Description = "12 - 1 lb pkgs.",
                            Name = "Uncle Bob's Organic Dried Pears",
                            Price = 100f
                        },
                        new
                        {
                            ID = 11,
                            CategoryID = 1,
                            Description = "24 - 12 oz cans",
                            Name = "Coca-Cola",
                            Price = 25f
                        },
                        new
                        {
                            ID = 12,
                            CategoryID = 1,
                            Description = "24 - 12 oz cans",
                            Name = "Pepsi",
                            Price = 25f
                        },
                        new
                        {
                            ID = 13,
                            CategoryID = 1,
                            Description = "100g jar",
                            Name = "Nescafe Coffee",
                            Price = 15f
                        },
                        new
                        {
                            ID = 14,
                            CategoryID = 1,
                            Description = "50 tea bags",
                            Name = "Green Tea",
                            Price = 12f
                        },
                        new
                        {
                            ID = 15,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Budweiser",
                            Price = 30f
                        },
                        new
                        {
                            ID = 16,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Heineken",
                            Price = 35f
                        },
                        new
                        {
                            ID = 17,
                            CategoryID = 2,
                            Description = "20 oz bottle",
                            Name = "Ketchup",
                            Price = 5f
                        },
                        new
                        {
                            ID = 18,
                            CategoryID = 2,
                            Description = "32 oz jar",
                            Name = "Mayonnaise",
                            Price = 8f
                        },
                        new
                        {
                            ID = 19,
                            CategoryID = 2,
                            Description = "8 oz bottle",
                            Name = "Mustard",
                            Price = 4f
                        },
                        new
                        {
                            ID = 20,
                            CategoryID = 2,
                            Description = "16 oz bottle",
                            Name = "Soy Sauce",
                            Price = 6f
                        },
                        new
                        {
                            ID = 21,
                            CategoryID = 3,
                            Description = "1 gallon",
                            Name = "Milk",
                            Price = 3f
                        },
                        new
                        {
                            ID = 22,
                            CategoryID = 3,
                            Description = "8 oz block",
                            Name = "Cheese",
                            Price = 4f
                        },
                        new
                        {
                            ID = 23,
                            CategoryID = 3,
                            Description = "6-pack",
                            Name = "Yogurt",
                            Price = 6f
                        },
                        new
                        {
                            ID = 24,
                            CategoryID = 3,
                            Description = "16 oz tub",
                            Name = "Butter",
                            Price = 5f
                        },
                        new
                        {
                            ID = 25,
                            CategoryID = 4,
                            Description = "20 oz loaf",
                            Name = "White Bread",
                            Price = 2f
                        },
                        new
                        {
                            ID = 26,
                            CategoryID = 4,
                            Description = "6-pack",
                            Name = "Croissant",
                            Price = 4f
                        },
                        new
                        {
                            ID = 27,
                            CategoryID = 4,
                            Description = "8-inch",
                            Name = "Chocolate Cake",
                            Price = 10f
                        },
                        new
                        {
                            ID = 28,
                            CategoryID = 5,
                            Description = "1 lb",
                            Name = "Apple",
                            Price = 1f
                        },
                        new
                        {
                            ID = 29,
                            CategoryID = 5,
                            Description = "1 lb",
                            Name = "Banana",
                            Price = 5f
                        },
                        new
                        {
                            ID = 30,
                            CategoryID = 5,
                            Description = "1 lb",
                            Name = "Orange",
                            Price = 75f
                        },
                        new
                        {
                            ID = 31,
                            CategoryID = 5,
                            Description = "1 lb",
                            Name = "Strawberries",
                            Price = 2f
                        },
                        new
                        {
                            ID = 32,
                            CategoryID = 6,
                            Description = "1 lb",
                            Name = "Beef",
                            Price = 8f
                        },
                        new
                        {
                            ID = 33,
                            CategoryID = 6,
                            Description = "1 lb",
                            Name = "Chicken",
                            Price = 6f
                        },
                        new
                        {
                            ID = 34,
                            CategoryID = 6,
                            Description = "1 lb",
                            Name = "Pork",
                            Price = 7f
                        },
                        new
                        {
                            ID = 35,
                            CategoryID = 6,
                            Description = "8 oz fillet",
                            Name = "Salmon",
                            Price = 12f
                        },
                        new
                        {
                            ID = 36,
                            CategoryID = 7,
                            Description = "12-inch",
                            Name = "Frozen Pizza",
                            Price = 8f
                        },
                        new
                        {
                            ID = 37,
                            CategoryID = 7,
                            Description = "1 pint",
                            Name = "Ice Cream",
                            Price = 5f
                        },
                        new
                        {
                            ID = 38,
                            CategoryID = 7,
                            Description = "16 oz bag",
                            Name = "Frozen Vegetables",
                            Price = 3f
                        },
                        new
                        {
                            ID = 39,
                            CategoryID = 1,
                            Description = "24 - 12 oz cans",
                            Name = "Coca-Cola Zero",
                            Price = 25f
                        },
                        new
                        {
                            ID = 40,
                            CategoryID = 1,
                            Description = "24 - 12 oz cans",
                            Name = "Sprite",
                            Price = 25f
                        },
                        new
                        {
                            ID = 41,
                            CategoryID = 1,
                            Description = "50 capsules",
                            Name = "Nespresso Coffee",
                            Price = 30f
                        },
                        new
                        {
                            ID = 42,
                            CategoryID = 1,
                            Description = "100 tea bags",
                            Name = "Black Tea",
                            Price = 10f
                        },
                        new
                        {
                            ID = 43,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Corona",
                            Price = 35f
                        },
                        new
                        {
                            ID = 44,
                            CategoryID = 1,
                            Description = "24 - 12 oz bottles",
                            Name = "Bud Light",
                            Price = 30f
                        },
                        new
                        {
                            ID = 45,
                            CategoryID = 2,
                            Description = "18 oz bottle",
                            Name = "Barbecue Sauce",
                            Price = 4f
                        },
                        new
                        {
                            ID = 46,
                            CategoryID = 2,
                            Description = "16 oz jar",
                            Name = "Salsa",
                            Price = 5f
                        },
                        new
                        {
                            ID = 47,
                            CategoryID = 2,
                            Description = "12 oz bottle",
                            Name = "Honey Mustard",
                            Price = 4f
                        },
                        new
                        {
                            ID = 48,
                            CategoryID = 2,
                            Description = "32 oz bottle",
                            Name = "Vinegar",
                            Price = 3f
                        },
                        new
                        {
                            ID = 49,
                            CategoryID = 3,
                            Description = "1 quart",
                            Name = "Almond Milk",
                            Price = 4f
                        },
                        new
                        {
                            ID = 50,
                            CategoryID = 3,
                            Description = "32 oz tub",
                            Name = "Greek Yogurt",
                            Price = 6f
                        });
                });

            modelBuilder.Entity("Shopping.Models.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AuthLevel")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AuthLevel = 9,
                            Email = "sss@ddd.com",
                            Name = "name name",
                            Password = "1234",
                            UserName = "user"
                        },
                        new
                        {
                            ID = 2,
                            AuthLevel = 2,
                            Email = "user2@ddd.com",
                            Name = "user2 user2",
                            Password = "1234",
                            UserName = "user2"
                        });
                });

            modelBuilder.Entity("Shopping.Models.Entities.Product", b =>
                {
                    b.HasOne("Shopping.Models.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Shopping.Models.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
