namespace ShopEasy.API.Dto
{
    public class AddToCartRequest
    {
        public string UserId { get; set; }

        public string GuestId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

    }

}
