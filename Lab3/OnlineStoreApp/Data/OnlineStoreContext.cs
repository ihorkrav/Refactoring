using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using OnlineStoreApp.Classes;
namespace OnlineStoreApp.Data
{
    public class OnlineStoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This creates OnlineStore.db in your project folder
            optionsBuilder.UseSqlite("Data Source=OnlineStore.db");
        }
    }
}
