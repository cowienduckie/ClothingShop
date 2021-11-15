using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class ProductCategory
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}