using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class ProductViewModel
    {
        [Display(Name = "ID")]
        public int ProductId { get; set; }

        [StringLength(50), Required]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Required, DataType(DataType.Currency)]
        [Display(Name = "Giá")]
        public int Price { get; set; }

        [Display(Name = "Số lượng")]
        public int Stock { get; set; }
    }
}