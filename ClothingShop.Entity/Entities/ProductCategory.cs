using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class ProductCategory
    {
        [Required] [Key] public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required] [Key] public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}