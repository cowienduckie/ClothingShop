using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class ProductEntry
    {
        [Required] [Key] public int SkuId { get; set; }

        [Required] [Key] public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required] [Key] public int ColorId { get; set; }

        public Color Color { get; set; }

        [Required] [Key] public int SizeId { get; set; }

        public Size Size { get; set; }

        [StringLength(30)] public string SKU { get; set; }

        [Required] public int Quantity { get; set; }

        //
        public IList<CartItem> CartItems { get; set; }

        public IList<OrderItem> OrderItems { get; set; }

        public ProductEntry Buy(int quantity)
        {
            Quantity -= quantity;

            return this;
        }
    }
}