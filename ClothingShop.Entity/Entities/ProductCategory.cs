using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class ProductCategory
    {
        [Required]
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Key]
        public int CategoryId { get; set; }
    }
}