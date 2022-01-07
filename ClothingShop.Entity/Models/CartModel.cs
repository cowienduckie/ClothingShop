namespace ClothingShop.Entity.Models
{
    public class CartModel
    {
        public int CartId { get; set; }

        public string UserId { get; set; }

        public int OriginalPrice { get; set; }

        public int Discount { get; set; }

        public int TotalPrice { get; set; }
    }
}