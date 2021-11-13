using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Product_Entry
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public int SizeId { get; set; }

        [StringLength(30)]
        #nullable enable
        public string? SKU { get; set; }
        #nullable disable

        [Required]
        public int Quantity { get; set; }
    }
}