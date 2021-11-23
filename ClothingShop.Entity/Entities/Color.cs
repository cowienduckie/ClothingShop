using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Color
    {
        [Required]
        public int ColorId { get; set; }

        //Each color has a unique code 
        [Required]
        public int Value { get; set; }
    }
}