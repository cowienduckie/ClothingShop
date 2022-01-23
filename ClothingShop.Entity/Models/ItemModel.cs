using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class ItemModel
    {
        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public int SkuId { get; set; }

        [Display(Name = "Màu sắc")] public string ColorValue { get; set; }

        public string ColorHexCode { get; set; }

        [Display(Name = "Kích cỡ")] public string SizeValue { get; set; }

        public string SKU { get; set; }

        [Display(Name = "Số lượng")] public int Quantity { get; set; }
    }
}