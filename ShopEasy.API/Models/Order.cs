namespace ShopEasy.API.Models
{
    public class Order
    {
        public required int OrderId { get; set; }
        public required int  CartId { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
