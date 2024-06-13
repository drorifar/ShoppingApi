using Microsoft.EntityFrameworkCore;
using Shopping.Entities;

namespace Shopping.Context
{
    public class MyDBContext : DbContext
    {
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("connectionstring");

            base.OnConfiguring(options);
        }
    }
}
