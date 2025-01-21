namespace FastFoodWeb.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItems> Items { get; set; } = new List<OrderItems>();
        public bool Completed { get; set; }

        public int CategoryId { get; set; }
    }
}
