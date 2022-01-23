using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class ReportBillingModel : PaginationModel<ReportBillingResultModel>
    {
        public ReportBillingModel()
        {
            ItemList = new List<ReportBillingResultModel>();
            PageNumber = 1;
            PageSize = 20;
            Total = 0;
        }

        [Display(Name = "Từ ngày")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; } = DateTime.MinValue;

        [Display(Name = "Đến ngày")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; } = DateTime.MaxValue;

        [Display(Name = "Mã đơn hàng")] public int? OrderId { get; set; }

        [Display(Name = "Mã người dùng")] public string UserId { get; set; }

        public bool IsExport { get; set; } = false;
    }
}