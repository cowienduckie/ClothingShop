using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class ProductViewModels
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, DataType(DataType.Currency)]
        public long Price { get; set; }
    }
}
