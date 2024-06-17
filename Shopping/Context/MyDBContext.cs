using Microsoft.EntityFrameworkCore;
using Shopping.Entities;

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
    }
}
