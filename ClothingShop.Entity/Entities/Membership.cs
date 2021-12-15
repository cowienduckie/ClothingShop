using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Membership
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int LevelId { get; set; }

        public bool IsValid { get; set; }
    }
}
