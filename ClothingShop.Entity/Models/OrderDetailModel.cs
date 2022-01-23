using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class OrderDetailModel
    {
        [Key] public int OrderId { get; set; }

        public string UserId { get; set; }

        public int OriginalPrice { get; set; }

        public int Discount { get; set; }

        public int TotalPrice { get; set; }

        public string Status { get; set; }

        [Required]
        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        public List<OrderItemModel> ListItem { get; set; }
    }
}