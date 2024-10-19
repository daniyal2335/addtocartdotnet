namespace AddToCart.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        public Product product { get; set; }
    }
}
