using FastFoodWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodWeb.Data
{
    public class FastFoodDbContext : DbContext
    {
        public FastFoodDbContext(DbContextOptions<FastFoodDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
