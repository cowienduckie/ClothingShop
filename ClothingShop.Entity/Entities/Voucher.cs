using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Voucher
    {
        public int VoucherId { get; set; }

        public int DiscountId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Mã voucher")] public string Value { get; set; }

        [Display(Name = "Trạng thái")] public bool IsUsed { get; set; }

        //
        public Discount Discount { get; set; }

        public Users User { get; set; }
    }
}