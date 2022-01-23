using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Order
    {
        [Key] [Display(Name = "ID Đơn hàng")] public int OrderId { get; set; }

        [Display(Name = "ID Tài khoản")] public string UserId { get; set; }

        public int PointId { get; set; }

        public int AddressId { get; set; }

        [Display(Name = "Giá gốc")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int OriginalPrice { get; set; }

        [Display(Name = "Chiết khấu")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int Discount { get; set; }

        [Display(Name = "Tổng giá")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int TotalPrice { get; set; }

        [Display(Name = "Trạng thái")] public string Status { get; set; }

        [Display(Name = "Ghi chú đơn hàng")]
        [MaxLength(500)]
        public string Note { get; set; }

        [Required]
        [Display(Name = "Ngày tạo đơn hàng")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Display(Name = "Ngày duyệt đơn hàng")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalTime { get; set; }

        //
        public Address Address { get; set; }

        public Point Point { get; set; }

        public Users User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}