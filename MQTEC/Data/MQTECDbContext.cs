using Microsoft.EntityFrameworkCore;
using MQTEC.Models;

namespace MQTEC.Data
{
    public class MQTECDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-NPSHI85\\EVERTONESC;Database=MQTEC;User Id=react;Password=react");
        }
    }
}
