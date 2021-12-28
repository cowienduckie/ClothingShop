using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ClothingShop.Entity.Entities
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, DataType(DataType.Currency)]
        public int Price { get; set; }

        [AllowNull]
        public int? DiscountId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Create Time"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "Last Modified"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        //
        public IList<ProductEntry> ProductEntries { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }

        public Discount Discount { get; set; }
    }
}