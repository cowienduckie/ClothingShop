using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Size
    {
        [Required]
        public int SizeId { get; set; }

        [Required]
        public int Value { get; set; }
    }
}