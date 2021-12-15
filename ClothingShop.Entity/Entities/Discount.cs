using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Discount
    {
        [Required]
        public int DiscountId { get; set; }
        
        [Required]
        public int VoucherId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Range(1, 100), Required]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Value { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}