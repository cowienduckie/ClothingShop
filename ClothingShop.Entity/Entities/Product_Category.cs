using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Product_Category
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}