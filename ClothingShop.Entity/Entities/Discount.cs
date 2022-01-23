using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Discount
    {
        public int DiscountId { get; set; }

        [Display(Name = "Tên")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Mã giảm giá")]
        [StringLength(50)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Chiết khấu")]
        [Range(0, 100)]
        [Required]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public int Percentage { get; set; }

        [Required]
        [Display(Name = "Hết hạn?")]
        public bool IsExpired { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Create Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "Last Modified")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        //
        public IList<ProductDiscount> ProductDiscounts { get; set; }

        public IList<Voucher> Vouchers { get; set; }
    }
}