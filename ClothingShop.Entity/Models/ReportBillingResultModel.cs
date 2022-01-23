using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class ReportBillingResultModel
    {
        [Display(Name = "Mã đơn hàng")] public int OrderId { get; set; }

        [Display(Name = "Mã người dùng")] public string UserId { get; set; }

        [Display(Name = "Tên khách hàng")] public string CustomerName { get; set; }

        [Display(Name = "Tên người nhận")] public string ReceiverName { get; set; }

        [Display(Name = "Địa chỉ")] public string Address { get; set; }

        [Display(Name = "Số điện thoại")] public string PhoneNumber { get; set; }

        [Display(Name = "Giá gốc")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int OriginalPrice { get; set; }

        [Display(Name = "Chiết khấu")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int DiscountAmount { get; set; }

        [Display(Name = "Tổng giá")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int TotalPrice { get; set; }

        [Display(Name = "Lập đơn hàng")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Display(Name = "Duyệt đơn hàng")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalTime { get; set; }

        [Display(Name = "Trạng thái đơn hàng")]
        public string OrderStatus { get; set; }

        [Display(Name = "Ghi chú")] public string Note { get; set; }
    }
}