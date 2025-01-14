using FastFoodWeb.Models;

namespace FastFoodWeb.Services
{
    public class DataService
    {
        public List<MenuItems> Menu { get; set; } = new()
        {
            new MenuItems { Id = 1, Name = "Hamburger", Price = 10.99m },
            new MenuItems { Id = 2, Name = "Frytki", Price = 5.99m },
            new MenuItems { Id = 3, Name = "Cola", Price = 3.99m },
        };

        public List<Orders> Orders { get; set; } = new();
    }
}
