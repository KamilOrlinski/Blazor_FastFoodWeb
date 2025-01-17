using System.Linq;

namespace FastFoodWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrderItems> Items { get; set; }

    }
    public class OrderItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
