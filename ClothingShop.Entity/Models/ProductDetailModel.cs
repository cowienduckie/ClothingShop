using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Models
{
    public class ProductDetailModel
    {
        public int ProductId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, DataType(DataType.Currency)]
        public int Price { get; set; }

        [Range(0, 100)]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? Discount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int Stock { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        public List<ItemModel> Items { get; set; }

        public ProductDetailModel()
        {
            this.Discount = 0;
            this.Image = "https://imgur.com/a/tZDUgE8";
            this.Items = new List<ItemModel>();
        }
    }
}