using Microsoft.AspNetCore.Http.Features;

namespace ShopEasy.API.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string? UserId { get; set; } 
        public string? GuestId { get; set; }
        public List<CartItem>? Items { get; set; }
        public decimal? CartSum { get => Items?.Sum(i => i.Quantity * i.Price); }
    }
}

