using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Level
    {
        [Required]
        public int LevelId { get; set; }

        [StringLength(30), Required]
        public string Name { get; set; }

        [Required]
        public int LevelPoint { get; set; }
    }
}
