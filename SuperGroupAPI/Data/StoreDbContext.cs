using Microsoft.EntityFrameworkCore;
using SuperGroupAPI.Models;

namespace SuperGroupAPI.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> OrderHistories { get; set; }
    }
}
