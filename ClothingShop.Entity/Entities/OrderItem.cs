using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothingShop.Entity.Entities
{
    public class OrderItem
    {
        [Required, Key]
        public int OrderItemId { get; set; }

        [Required, Key]
        public int SkuId { get; set; }

        [Required, Key]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Create Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "LastModified Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        //
        public ProductEntry SKU { get; set; }
        public Order Order { get; set; }
    }
}
