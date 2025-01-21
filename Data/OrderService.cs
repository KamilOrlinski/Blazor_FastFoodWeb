using FastFoodWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FastFoodWeb.Data
{
    public class OrderService
    {
        public List<Category> Categories { get; set; } = new List<Category>
        {
            new Category
            {
                Name = "Burgery",
                Items = new List<OrderItems>
                {
                    new OrderItems { Name = "Hamburger", Price = 10.99m },
                    new OrderItems { Name = "Cheeseburger", Price = 12.99m },
                    new OrderItems { Name = "Kurczakburger", Price = 12.99m }
                }
            },
            new Category
            {
                Name = "Napoje",
                Items = new List<OrderItems>
                {
                    new OrderItems { Name = "Cola", Price = 3.99m },
                    new OrderItems { Name = "Fanta", Price = 4.99m },
                    new OrderItems { Name = "Lipton", Price = 4.99m }
                }
            },
            new Category
            {
                Name = "Dodatki",
                Items = new List<OrderItems>
                {
                    new OrderItems { Name = "Frytki małe", Price = 5.99m },
                    new OrderItems { Name = "Frytki duże", Price = 6.99m },
                    new OrderItems { Name = "Sałatka", Price = 7.99m }
                }
            }
        };

        public List<OrderItems> CurrentOrder { get; set; } = new List<OrderItems>();

        public decimal TotalAmount => CurrentOrder.Sum(item => item.Price * item.Quantity);

        public void AddToOrder(string name, decimal price, int quantity)
        {
            var existingItem = CurrentOrder.FirstOrDefault(item => item.Name == name);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

            }
            else
            {
                CurrentOrder.Add(new OrderItems
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity
                });
            }
        }

        public void ClearOrder()
        {
            CurrentOrder.Clear();
        }

        public List<OrderItems> GetItemsByCategory(string categoryName)
        {
            var category = Categories.FirstOrDefault(c => c.Name == categoryName);
            return category?.Items ?? new List<OrderItems>();
        }

        public decimal GetTotalPrice()
        {
            return CurrentOrder.Sum(item => item.Price * item.Quantity);
        }

        public async Task InitializeDatabase(FastFoodDbContext dbContext)
        {

            if (!dbContext.Categories.Any())
            {

                foreach (var category in Categories)
                {
                    var dbCategory = new Category { Name = category.Name };
                    dbContext.Categories.Add(dbCategory);
                    await dbContext.SaveChangesAsync();

                    foreach (var item in category.Items)
                    {
                        dbContext.OrderItems.Add(new OrderItems
                        {
                            Name = item.Name,
                            Price = item.Price,
                            CategoryId = dbCategory.Id
                        });
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private readonly FastFoodDbContext _context;

        public OrderService(FastFoodDbContext context)

        {
            _context = context;
        }

        public async Task SaveOrderAsync()
        {
            if (CurrentOrder == null || !CurrentOrder.Any())
                throw new InvalidOperationException("Zamówienie jest puste!");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Orders
                {
                    OrderDate = DateTime.Now,
                    TotalAmount = TotalAmount,
                    Items = CurrentOrder.Select(item => new OrderItems
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        CategoryId = item.CategoryId
                    }).ToList()
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                ClearOrder();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
