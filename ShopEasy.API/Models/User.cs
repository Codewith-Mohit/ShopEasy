namespace ShopEasy.API.Models
{
    public class User 
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Guest"; //default role type

        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
