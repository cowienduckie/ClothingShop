using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class ProductEntry
    {
        [Required]
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Key]
        public int ColorId { get; set; }

        [Required]
        [Key]
        public int SizeId { get; set; }

        [StringLength(30)]
        public string SKU { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}