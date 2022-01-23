using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        [Required]
        [Key]
        [Display(Name = "ID Giỏ hàng")]
        public int CartId { get; set; }

        [Display(Name = "ID Tài khoản")] public string UserId { get; set; }

        public int? VoucherId { get; set; }

        [Display(Name = "Giá gốc")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int OriginalPrice { get; set; }

        [Display(Name = "Chiết khấu")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int Discount { get; set; }

        [Display(Name = "Tổng giá")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int TotalPrice { get; set; }

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

        [Required] public bool IsDelete { get; set; }

        //
        public Users User { get; set; }

        public Voucher Voucher { get; set; }

        public IList<CartItem> CartItems { get; set; }
    }
}