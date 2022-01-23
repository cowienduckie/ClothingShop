using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }

        public int SkuId { get; set; }

        [StringLength(50)] [Required] public string Name { get; set; }

        [Required] public string Image { get; set; }

        public int OrderId { get; set; }

        [Required] public int Quantity { get; set; }
    }
}