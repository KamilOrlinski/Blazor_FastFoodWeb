namespace FastFoodWeb.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public List<MenuItems> OrderItems { get; set; }
        public bool Completed { get; set; }
    }
}
