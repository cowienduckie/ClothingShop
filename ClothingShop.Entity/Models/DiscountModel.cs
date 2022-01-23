using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class DiscountModel
    {
        public DiscountModel()
        {
            UsedVoucherNumber = 0;
            UnUsedVoucherNumber = 0;
            OwnedVoucherNumber = 0;
            EndTime = DateTime.MaxValue;
        }

        public int DiscountId { get; set; }

        [Display(Name = "Tên khuyến mãi")] public string Name { get; set; }

        [Display(Name = "Mã khuyến mãi")] public string Code { get; set; }

        [Display(Name = "Chiết khấu (%)")]
        [Range(0, 100)]
        [Required]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public int Percentage { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Hết hạn?")] public bool IsExpired { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        public int UsedVoucherNumber { get; set; }

        public int UnUsedVoucherNumber { get; set; }

        public int OwnedVoucherNumber { get; set; }
    }
}