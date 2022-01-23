using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class ProductDiscount
    {
        [Required] [Key] public int ProductId { get; set; }

        [Required] [Key] public int DiscountId { get; set; }

        //
        public Product Product { get; set; }

        public Discount Discount { get; set; }
    }
}