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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Burgery" },
                new Category { Id = 2, Name = "Napoje" },
                new Category { Id = 3, Name = "Dodatki" }
            );

            // Seed data for OrderItems
            modelBuilder.Entity<OrderItems>().HasData(
                new OrderItems { Id = 1, Name = "Hamburger", Price = 10.99m, CategoryId = 1, OrdersId = null },
                new OrderItems { Id = 2, Name = "Cheeseburger", Price = 12.99m, CategoryId = 1, OrdersId = null },
                new OrderItems { Id = 3, Name = "Kurczakburger", Price = 12.99m, CategoryId = 1, OrdersId = null },
                new OrderItems { Id = 4, Name = "Cola", Price = 3.99m, CategoryId = 2, OrdersId = null },
                new OrderItems { Id = 5, Name = "Fanta", Price = 4.99m, CategoryId = 2, OrdersId = null },
                new OrderItems { Id = 6, Name = "Lipton", Price = 4.99m, CategoryId = 2, OrdersId = null },
                new OrderItems { Id = 7, Name = "Frytki małe", Price = 5.99m, CategoryId = 3, OrdersId = null },
                new OrderItems { Id = 8, Name = "Frytki duże", Price = 6.99m, CategoryId = 3, OrdersId = null },
                new OrderItems { Id = 9, Name = "Sałatka", Price = 7.99m, CategoryId = 3, OrdersId = null }
            );

            modelBuilder.Entity<OrderItems>()
                .Property(o => o.OrdersId)
                .IsRequired(false);
        }
    }
}
