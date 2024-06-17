using Microsoft.EntityFrameworkCore;
using Shopping.Entities;
using Shopping.Models;

namespace Shopping.Context
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }


        // we move the configuration to the program.cs
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite("connectionstring"); // extension method. added when we added the sqlite nugget pkg (we can change it to another DB)

        //    base.OnConfiguring(options);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category()
                    {
                        ID = 1,
                        Name = "Name",
                        Description = "Description",
                    },
                    new Category()
                    {
                        ID = 2,
                        Name = "Name 2",
                        Description = "Description 2",
                    }
                );

            modelBuilder.Entity<Product>().HasData(
                    new Product { ID = 1, Name = "Chai", Description = "10 boxes x 20 bags", Price = 10, CategoryID = 1},
                    new Product { ID = 2, Name = "Chang", Description = "24 - 12 oz bottles", Price = 20, CategoryID = 1 },
                    new Product { ID = 3, Name = "Guaraná Fantástica", Description = "12 - 355 ml cans", Price = 30, CategoryID = 1 },
                    new Product { ID = 4, Name = "Sasquatch Ale", Description = "24 - 12 oz bottles", Price = 40, CategoryID = 2 },
                    new Product { ID = 5, Name = "Steeleye Stout", Description = "24 - 12 oz bottles", Price = 50, CategoryID = 2 },
                    new Product { ID = 6, Name = "Chai2", Description = "10 boxes x 20 bags", Price = 10, CategoryID = 1 },
                    new Product { ID = 7, Name = "Chang2", Description = "24 - 12 oz bottles", Price = 20, CategoryID = 1 },
                    new Product { ID = 8, Name = "Guaraná Fantástica2", Description = "12 - 355 ml cans", Price = 30, CategoryID = 1 },
                    new Product { ID = 9, Name = "Sasquatch Ale2", Description = "24 - 12 oz bottles", Price = 40, CategoryID = 2 },
                    new Product { ID = 10, Name = "Steeleye Stout2", Description = "24 - 12 oz bottles", Price = 50, CategoryID = 2 }
                );

            base.OnModelCreating(modelBuilder);

        }
    }
}
